using ATS.Common.DataModel;
using System;

namespace ATS.Common.Specification
{
    public class DepartureCitySpecification : Specification<Tour>
    {
        private readonly string _city;

        public DepartureCitySpecification(string city)
        {
            _city = city ?? throw new ArgumentNullException(nameof(city));
        }

        public override bool IsSatisfied(Tour item)
        {
            return item.DepartureCity.Name.ToLower() == _city.ToLower();
        }
    }
}
