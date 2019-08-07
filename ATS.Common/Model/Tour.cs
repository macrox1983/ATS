using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Model
{
    public class Tour
    {
        public Tour(int id, string providerName, string hotelName, string roomTypeName, 
            string departureCityName, string airlineName, DateTime departureDate, DateTime arrivalDate, 
            DateTime hotelCheckInDate, int hotelNightCount, decimal hotelNightPriceByPerson, int personMaxCount)
        {
            Id = id;
            ProviderName = providerName ?? throw new ArgumentNullException(nameof(providerName));
            HotelName = hotelName ?? throw new ArgumentNullException(nameof(hotelName));
            RoomTypeName = roomTypeName ?? throw new ArgumentNullException(nameof(roomTypeName));
            DepartureCityName = departureCityName ?? throw new ArgumentNullException(nameof(departureCityName));
            AirlineName = airlineName ?? throw new ArgumentNullException(nameof(airlineName));
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            HotelCheckInDate = hotelCheckInDate;
            HotelNightCount = hotelNightCount;
            HotelNightPriceByPerson = hotelNightPriceByPerson;
            PersonMaxCount = personMaxCount;
        }

        public int Id { get; set; }

        public string ProviderName { get; set; }

        public string HotelName { get; set; }

        public string RoomTypeName { get; set; }

        public string DepartureCityName { get; set; }

        public string AirlineName { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime HotelCheckInDate { get; set; }

        public int HotelNightCount { get; set; }
        
        public decimal HotelNightPriceByPerson { get; set; }

        public int  PersonMaxCount { get; set; }
    }
}
