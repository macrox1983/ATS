using ATS.Common.DataModel;
using System;

namespace ATS.Common.Specification
{
    public class TourCitySpecification : Specification<Tour>
    {
        private readonly string _city;

        public TourCitySpecification(string city)
        {
            _city = city ?? throw new ArgumentNullException(nameof(city));
        }

        public override bool IsSatisfied(Tour item)
        {
            return item.Hotel.City.Name.ToLower() == _city.ToLower();
        }
    }
}
