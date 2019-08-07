using ATS.Common;
using ATS.Common.Messages;
using ATS.Common.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATS.AggregateSearchService
{
    public class AggregateSearchService : IHostedService, IAggregateSearchService
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IOptions<AppSettings> _options;
        private ConcurrentDictionary<Guid, List<Tour>> _results;
        private ConcurrentDictionary<Guid, int> _providerNotResponded;
        private CancellationTokenSource _delayTokenSource;

        public AggregateSearchService(IMessageBusClient messageBusClient, IOptions<AppSettings> options)
        {
            _messageBusClient = messageBusClient??throw new ArgumentNullException(nameof(messageBusClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _results = new ConcurrentDictionary<Guid, List<Tour>>();
            _providerNotResponded = new ConcurrentDictionary<Guid, int>();
            _delayTokenSource = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(Subscribe());                 
        }

        private bool Subscribe()
        {
            _messageBusClient.Subscribe<ToursResponse>(ReceiveResponse);
            return true;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(Stop());
        }

        private bool Stop()
        {
            return true;
        }

        public async Task<List<Tour>> Search(SearchPattern searchPattern)
        {
            var request = new ToursRequest(Guid.NewGuid())
            {                
                SearchPattern = searchPattern
            };
            // делаем так для того чтобы прервать ожидание, если все провайдеры ответили до истечения таймаута
            _providerNotResponded.TryAdd(request.RequestId, _messageBusClient.GetSubscriberCount<ToursRequest>());

            //отправляем поисковый запрос 
            _messageBusClient.SendMessage(request);

            // ожидаем ответа от всех провайдеров, если ответили все до истечения таймаута ожидание будет отменено
            try
            {
                await Task.Delay(_options.Value.RequestTimeout, _delayTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }            

            List<Tour> result = new List<Tour>();
            _results.TryRemove(request.RequestId, out result);

            return HandleResults(result, (ToursOrderEnum)searchPattern.ToursOrder);
        }

        /// <summary>
        /// Функция окончательной обработки результатов запросов к провайдерам
        /// </summary>
        /// <param name="result"></param>
        /// <param name="toursOrder"></param>
        /// <returns></returns>
        private List<Tour> HandleResults(List<Tour> result, ToursOrderEnum toursOrder)
        {
            ConcurrentBag<Tour> tours = new ConcurrentBag<Tour>();

            // группируем результат по полям и сортируем туры в группе
            var groups = result.AsParallel().GroupBy(tour=> new { tour.HotelName, tour.DepartureCityName, tour.DepartureDate, tour.ArrivalDate, tour.HotelCheckInDate, tour.HotelNightCount, tour.RoomTypeName},(key, values)=>values.OrderBy(v=>v.HotelNightPriceByPerson)).ToArray();

            Parallel.ForEach(groups, group => 
            {
                if (group.Count() > 1)
                {
                    //Tour preferredTour;
                    Tour minPriceTour = group.First(); //берем минимальный тур по цене
                                                       //!!! тут не понятна в ТЗ суть обработки получается что берем всегда предпочитаемый, 
                                                       // если он между мин и макс ценой, тк сказанно что разница со всеми не более x %,
                                                       // может быть разница между ним и всеми, которые дешевле его                    
                    if (minPriceTour.ProviderName.ToLower() == _options.Value.PreferredProvider.ToLower())// если минимальный по цене тур от предпочитаемого провайдера то
                    {                                                                                     
                        tours.Add(minPriceTour);
                    }
                    else
                    {
                        //поскольку в ТЗ непонятно как сравнивать будем со следующим который дешевле, тк с другими смысла нет
                        var preferredTour = group //найдем предпочитаемый тур и возмем его индекс
                            .AsParallel()
                            .Where(tour => tour.ProviderName.ToLower() == _options.Value.PreferredProvider.ToLower())
                            .Select((tour, index) => new { index, tour }).FirstOrDefault();
                        if (preferredTour.index == 1 //значит предпочитаемый тур следующий за минимальным тк список отсортирован, сравним цену
                            && ((preferredTour.tour.HotelNightPriceByPerson - minPriceTour.HotelNightPriceByPerson) / minPriceTour.HotelNightPriceByPerson) * 100 <= _options.Value.DifferenceBetweenProviderPrice)
                        {
                            tours.Add(preferredTour.tour);
                        }
                        else
                        {
                            var cheaperTour = group.ElementAtOrDefault(preferredTour.index - 1);
                            if (((preferredTour.tour.HotelNightPriceByPerson - cheaperTour.HotelNightPriceByPerson) / cheaperTour.HotelNightPriceByPerson) * 100 <= _options.Value.DifferenceBetweenProviderPrice)
                                tours.Add(preferredTour.tour);
                            else
                                tours.Add(minPriceTour);
                        }
                    }
                }
                else
                {
                    tours.Add(group.First());
                }
                                
            });

            //сортируем список (по умолчанию по цене)
            return tours.OrderBy(tour => tour, new TourComparer(toursOrder)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private void ReceiveResponse(ToursResponse response)
        {
            List<Tour> result = null;
            if (_results.ContainsKey(response.RequestId)
                && _results.TryGetValue(response.RequestId, out result)
                && result != null)
            {
                var newResult = new List<Tour>(result);
                newResult.AddRange(response.Data);
                //тут же сортируем данные
                _results.TryUpdate(response.RequestId, newResult, result);
            }
            else
                _results.TryAdd(response.RequestId, response.Data);
            if (_providerNotResponded.ContainsKey(response.RequestId))
            {
                int providerNotResponded = _providerNotResponded[response.RequestId];
                int newValue = providerNotResponded - 1;
                _providerNotResponded.TryUpdate(response.RequestId, newValue, providerNotResponded);
                if (_providerNotResponded[response.RequestId] == 0)
                {
                    _providerNotResponded.TryRemove(response.RequestId, out int value);                    
                    _delayTokenSource.Cancel();
                }
            }            
        }
    }
}
