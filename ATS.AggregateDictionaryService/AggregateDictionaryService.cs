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

namespace ATS.AggregateDictionaryService
{
    public class AggregateDictionariesService : IHostedService, IAggregateDictionariesService
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IOptions<AppSettings> _options;
        private CancellationTokenSource _delayTokenSource;
        private ConcurrentDictionary<Guid, int> _providerNotResponded;
        private ConcurrentDictionary<Guid, object> _results;

        public AggregateDictionariesService(IMessageBusClient messageBusClient, IOptions<AppSettings> options)
        {
            _messageBusClient = messageBusClient ?? throw new ArgumentNullException(nameof(messageBusClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _results = new ConcurrentDictionary<Guid, object>();
            _providerNotResponded = new ConcurrentDictionary<Guid, int>();
            _delayTokenSource = new CancellationTokenSource();
        }

        private async Task<T> DoRequest<T>(Request request)
        {
            // делаем так для того чтобы прервать ожидание, если все провайдеры ответили до истечения таймаута
            _providerNotResponded.TryAdd(request.RequestId, _messageBusClient.GetSubscriberCount(request.GetType()));
            _messageBusClient.SendMessage(request);
            await Wait();

            return GetResult<T>(request.RequestId);
        }

        private T GetResult<T>(Guid requestId)
        {
            _results.TryRemove(requestId, out object result);
            return (T)result;
        }

        /// <summary>
        /// Метод для ожидания ответа от всех провайдеров, если ответили все до истечения таймаута ожидание будет отменено
        /// по истечению таймаута вернется результат от ответивших провайдеров
        /// </summary>
        /// <returns></returns>
        private async Task Wait()
        {
            try
            {
                await Task.Delay(_options.Value.RequestTimeout, _delayTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// Метод прекращающий ожидание
        /// </summary>
        private void CancelWait()
        {
            _delayTokenSource.Cancel();
        }

        public async Task<List<City>> GetAllCities()
        {
            var request = new CitiesRequest(Guid.NewGuid());
            List<City> cities = await DoRequest<List<City>>(request);
            //берем только уникальные записи от всех провайдеров
            return cities.GetUniqueElements(c=>c.Name);
        }

        public async Task<List<Country>> GetAllCountries()
        {
            var request = new CountriesRequest(Guid.NewGuid());
            List<Country> countries = await DoRequest<List<Country>>(request);
            //берем только уникальные записи от всех провайдеров
            return countries.GetUniqueElements(c=>c.Name);
        }

        public async Task<List<City>> GetAllDepartureCities()
        {
            var request = new DepartureCitiesRequest(Guid.NewGuid());
            List<City> departureCities = await DoRequest<List<City>>(request);
            //берем только уникальные записи от всех провайдеров
            return departureCities.GetUniqueElements(c => c.Name);
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var request = new HotelsRequest(Guid.NewGuid());
            List<Hotel> hotels = await DoRequest<List<Hotel>>(request);
            //берем только уникальные записи от всех провайдеров
            return hotels.GetUniqueElements(h=>h.Name);
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var request = new HotelRequest(Guid.NewGuid(), id);
            //ожидаем список тк у разных провайдеров может быть отели с одинаковым ид 
            //(тк ид типа int, решит проблему использование Guid, но будет не удобно генерировать данные)
            //пока просто возьмем первый из списка
            //но нужно дополнительный параметр в виде провайдера или ид типа Guid
            List<Hotel> hotels = await DoRequest<List<Hotel>>(request);            
            return hotels.FirstOrDefault();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(Subscribe());
        }

        private bool Subscribe()
        {
            _messageBusClient.Subscribe<DepartureCitiesResponse>(ReceiveDepartureCitiesResponse);
            _messageBusClient.Subscribe<CountriesResponse>(ReceiveCountriesResponse);
            _messageBusClient.Subscribe<CitiesResponse>(ReceiveCitiesResponse);
            _messageBusClient.Subscribe<HotelsResponse>(ReceiveHotelsResponse);
            _messageBusClient.Subscribe<HotelResponse>(ReceiveHotelResponse);
            return true;
        }

        private void ReceiveHotelResponse(HotelResponse response)
        {
            PutResult(response);
        }

        private void ReceiveHotelsResponse(HotelsResponse response)
        {
            PutResult(response);
        }

        private void ReceiveCitiesResponse(CitiesResponse response)
        {
            PutResult(response);
        }

        private void ReceiveCountriesResponse(CountriesResponse response)
        {
            PutResult(response);
        }

        private void ReceiveDepartureCitiesResponse(DepartureCitiesResponse response)
        {
            PutResult(response);
        }

        private void PutResult<T>(Response<T> response)
        {
            object result;
            if (_results.ContainsKey(response.RequestId)
                && _results.TryGetValue(response.RequestId, out result)
                && result != null)
            {
                var newResult = new List<T>((List<T>)result);
                newResult.AddRange(response.Data);
                _results.TryUpdate(response.RequestId, newResult, result);
            }
            else
            {
                //при попытке добавления нового результата по запросу повторно проверяем возможно успел добавиться результат от другого провайдера
                //если так то добавляем к существующему
                if (!_results.TryAdd(response.RequestId, response.Data) && _results.ContainsKey(response.RequestId))
                {
                    if(_results.TryGetValue(response.RequestId, out result) && result != null)
                    {
                        var newResult = new List<T>((List<T>)result);
                        newResult.AddRange(response.Data);
                        _results.TryUpdate(response.RequestId, newResult, result);
                    }
                }                    
            }
            if (_providerNotResponded.ContainsKey(response.RequestId))
            {
                int providerNotResponded = _providerNotResponded[response.RequestId];
                int newValue = providerNotResponded - 1;
                _providerNotResponded.TryUpdate(response.RequestId, newValue, providerNotResponded);
                if (_providerNotResponded[response.RequestId] == 0)
                {
                    _providerNotResponded.TryRemove(response.RequestId, out int value);
                    CancelWait();
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(Stop());
        }

        private bool Stop()
        {
            return true;
        }
    }
}
