using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.DataModel
{
    public class Hotel
    {
        public Hotel(int id, string name, string address, int cityId, int buildingYear, City city)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            CityId = cityId;
            BuildingYear = buildingYear;
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }

        public int BuildingYear { get; set; }

        public virtual City City { get; set; }

        public override string ToString()
        {
            return $"[{Id}, {Name}, {Address}, {BuildingYear}, {City}]";
        }
    }
}
