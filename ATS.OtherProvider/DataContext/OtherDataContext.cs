using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ATS.Common;
using ATS.Common.DataModel;
using ATS.OtherProvider.Abstraction;
using City = ATS.Common.DataModel.City;
using Country = ATS.Common.DataModel.Country;
using Hotel = ATS.Common.DataModel.Hotel;
using Tour = ATS.Common.DataModel.Tour;

namespace ATS.OtherProvider
{
    /// <summary>
    /// Класс хранящий данные о турах и справочники
    /// </summary>
    public class OtherDataContext : IOtherDataContext
    {
        private bool _initialized;

        /// <summary>
        /// Авиакомпании
        /// </summary>
        public Dictionary<int,Airline> Airlines { get; private set; }

        /// <summary>
        /// Города
        /// </summary>
        public Dictionary<int,City> Cities { get ; private set; }

        /// <summary>
        /// Страны
        /// </summary>
        public Dictionary<int,Country> Countries { get; private set; }

        /// <summary>
        /// Отели
        /// </summary>
        public Dictionary<int,Hotel> Hotels { get; private set; }

        /// <summary>
        /// Провайдеры туров
        /// </summary>
        public Dictionary<int,Provider> Providers { get; private set; }

        /// <summary>
        /// Типы номеров
        /// </summary>
        public Dictionary<int,RoomType> RoomTypes { get; private set; }

        /// <summary>
        /// Туры
        /// </summary>
        public Dictionary<int,Tour> Tours { get; private set; }

        /// <summary>
        /// Метод инициализации данных
        /// </summary>
        /// <param name="cityCount">количество городов</param>
        /// <param name="hotelCount">количество отелей</param>
        /// <param name="tourCount">количество туров</param>
        /// <param name="airlineCount">количество авиакомпаний</param>
        public async Task InitializeDataContext(int cityCount, int hotelCount, int tourCount, int airlineCount)
        {
            await Task.FromResult(Initialize(cityCount, hotelCount, tourCount, airlineCount));            
        }

        private bool Initialize(int cityCount, int hotelCount, int tourCount, int airlineCount)
        {
            if (!_initialized)
            {
                Countries = new Dictionary<int, Country>
            {
                { 0, new Country(0,"Россия") },
                { 1, new Country(1,"Туркменистан") },
                { 2, new Country(2,"Бангладеш") },
                { 3, new Country(3,"Австралия") },
                { 4, new Country(4,"США") },
                { 5, new Country(5,"Венесуэла") },
                { 6, new Country(6,"Тунис") },
                { 7, new Country(7,"Италия") },
                { 8, new Country(8,"Латвия") },
                { 9, new Country(9,"Финляндия") },
                { 10, new Country(10,"Швеция") },
            };

                var random = new Random();

                Cities = new Dictionary<int, City>();
                for (int i = 0; i < cityCount; i++)
                {
                    Country country = Countries[random.Next(11)];// 11-количество стран в выборке
                    Cities.Add(i, new City(i, $"Город{i}", country.Id, country));
                }

                Airlines = new Dictionary<int, Airline>();
                for (int i = 0; i < airlineCount; i++)
                {
                    Airlines.Add(i, new Airline(i, $"Авиакомпания{i}"));
                }

                Providers = new Dictionary<int, Provider>()
            {
                { 0, new Provider(0, "OtherProvider")}
            };

                RoomTypes = new Dictionary<int, RoomType>()
            {
                { 0, new RoomType(0,"стандарт")},
                { 1, new RoomType(1,"полу-люкс")},
                { 2, new RoomType(2,"люкс")}
            };

                Hotels = new Dictionary<int, Hotel>();
                for (int i = 0; i < hotelCount; i++)
                {
                    City city = Cities[random.Next(cityCount)];
                    Hotels.Add(i, new Hotel(i, $"Отель{i}", $"{city.Country.Name}, г.{city.Name}, ул.Отельная{i}, д.{random.Next(1, 300)}", city.Id, random.Next(1965, 2019), city));
                }

                Tours = new Dictionary<int, Tour>();

                for (int i = 0; i < tourCount; i++)
                {
                    Provider provider = Providers[0];
                    Hotel hotel = Hotels[random.Next(hotelCount)];
                    RoomType roomType = RoomTypes[random.Next(3)];
                    City departureCity = Cities[random.Next(cityCount)];
                    Airline airline = Airlines[random.Next(airlineCount)];
                    DateTime departureDate = DateTime.Now.AddDays(random.Next(180));
                    DateTime arrivalDate = departureDate.AddDays(Math.Round(random.Next(1, 100) / 100d));
                    int hotelNightCount = random.Next(5, 21);
                    decimal hotelNightPriceByPerson = (decimal)(random.NextDouble() + 0.01)
                        * random.Next(1000, 15000);
                    int personMaxCount = random.Next(1, 5);
                    Tours.Add(i, new Tour(i, provider.Id, hotel.Id, roomType.Id, departureCity.Id, airline.Id,
                        departureDate, arrivalDate, arrivalDate, hotelNightCount, hotelNightPriceByPerson, personMaxCount,
                        provider, hotel, roomType, departureCity, airline));
                }

                _initialized = true;
            }

            return _initialized;
        }
    }
}
