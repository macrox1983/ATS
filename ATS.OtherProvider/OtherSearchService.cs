using ATS.Common;
using ATS.Common.Messages;
using ATS.Common.Model;
using ATS.Common.Specification;
using ATS.OtherProvider.Abstraction;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATS.OtherProvider
{
    public class OtherSearchService : IHostedService
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IOtherTourRepository _tourRepository;
        private readonly IOtherDictionariesRepository _dictionariesRepository;

        public OtherSearchService(IMessageBusClient messageBusClient, IOtherTourRepository tourRepository, IOtherDictionariesRepository dictionariesRepository)
        {
            _messageBusClient = messageBusClient ?? throw new ArgumentNullException(nameof(messageBusClient));
            _tourRepository = tourRepository ?? throw new ArgumentNullException(nameof(tourRepository));
            _dictionariesRepository = dictionariesRepository ?? throw new ArgumentNullException(nameof(dictionariesRepository));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //инициализируем данные по конкретному провайдеру
            await _tourRepository.InitializeDataContext();
            await _dictionariesRepository.InitializeDataContext();
            //подписываемся на получение запроса на поиск через шину
            _messageBusClient.Subscribe<ToursRequest>(async request=>await ReceiveRequest(request));
            _messageBusClient.Subscribe<CitiesRequest>(async request => await ReceiveCitiesRequest(request));
            _messageBusClient.Subscribe<CountriesRequest>(async request => await ReceiveCountriesRequest(request));
            _messageBusClient.Subscribe<DepartureCitiesRequest>(async request => await ReceiveDepartureCitiesRequest(request));
            _messageBusClient.Subscribe<HotelRequest>(async request => await ReceiveHotelRequest(request));
            _messageBusClient.Subscribe<HotelsRequest>(async request => await ReceiveHotelsRequest(request));
        }

        private async Task ReceiveHotelsRequest(HotelsRequest request)
        {
            var hotels = await _dictionariesRepository.GetAllHotels();
            _messageBusClient.SendMessage(new HotelsResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = hotels.Select(hotel => new Hotel
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    CityName = hotel.City.Name,
                    Address = hotel.Address,
                    BuildingYear = hotel.BuildingYear
                }).ToList()
            });
        }

        private async Task ReceiveHotelRequest(HotelRequest request)
        {
            var hotel = await _dictionariesRepository.GetHotel(request.HotelId);
            _messageBusClient.SendMessage(new HotelResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = new List<Hotel>{ new Hotel
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    CityName = hotel.City.Name,
                    Address = hotel.Address,
                    BuildingYear = hotel.BuildingYear
                } }
            });
        }

        private async Task ReceiveDepartureCitiesRequest(DepartureCitiesRequest request)
        {
            var cities = await _dictionariesRepository.GetAllDepartureCities();
            _messageBusClient.SendMessage(new DepartureCitiesResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = cities.Select(city => new City
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId
                }).ToList()
            });
        }

        private async Task ReceiveCountriesRequest(CountriesRequest request)
        {
            var countries = await _dictionariesRepository.GetAllCountries();
            _messageBusClient.SendMessage(new CountriesResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = countries.Select(country => new Country
                {
                    Id = country.Id,
                    Name = country.Name
                }).ToList()
            });
        }

        private async Task ReceiveCitiesRequest(CitiesRequest request)
        {
            var cities = await _dictionariesRepository.GetAllCities();
            _messageBusClient.SendMessage(new CitiesResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = cities.Select(city => new City
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId
                }).ToList()
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(Stop());
        }

        private bool Stop()
        {
            return true;
        }

        private async Task ReceiveRequest(ToursRequest request)
        {
            var tours = await _tourRepository.Search(SpecificationHelper.GetSpecification(request.SearchPattern));

            _messageBusClient.SendMessage(new ToursResponse(request.RequestId, nameof(OtherSearchService))
            {
                Data = tours.Select(t => new Tour(t.Id, t.Provider.Name, t.Hotel.Name, t.RoomType.Name,
                t.DepartureCity.Name, t.Airline.Name, t.DepartureDate, t.ArrivalDate, t.HotelCheckInDate, t.HotelNightCount,
                t.HotelNightPriceByPerson, t.PersonMaxCount)).ToList()
            });
        }
    }
}
