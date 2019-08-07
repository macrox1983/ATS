using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.DataModel
{
    public class Tour
    {
        public Tour(int id, int providerId, int hotelId, int roomTypeId, 
            int departureCityId, int airlineId, DateTime departureDate, 
            DateTime arrivalDate, DateTime hotelCheckInDate, int hotelNightCount, 
            decimal hotelNightPriceByPerson, int personMaxCount, Provider provider, 
            Hotel hotel, RoomType roomType, City departureCity, Airline airline)
        {
            Id = id;
            ProviderId = providerId;
            HotelId = hotelId;
            RoomTypeId = roomTypeId;
            DepartureCityId = departureCityId;
            AirlineId = airlineId;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            HotelCheckInDate = hotelCheckInDate;
            HotelNightCount = hotelNightCount;
            HotelNightPriceByPerson = hotelNightPriceByPerson;
            PersonMaxCount = personMaxCount;
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Hotel = hotel ?? throw new ArgumentNullException(nameof(hotel));
            RoomType = roomType ?? throw new ArgumentNullException(nameof(roomType));
            DepartureCity = departureCity ?? throw new ArgumentNullException(nameof(departureCity));
            Airline = airline ?? throw new ArgumentNullException(nameof(airline));
        }

        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }

        public int DepartureCityId { get; set; }

        public int AirlineId { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime HotelCheckInDate { get; set; }

        public int HotelNightCount { get; set; }

        public decimal HotelNightPriceByPerson { get; set; }

        public int PersonMaxCount { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual City DepartureCity { get; set; }

        public virtual Airline Airline { get; set; }
    }
}
