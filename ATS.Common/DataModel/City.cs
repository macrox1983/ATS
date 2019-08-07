using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.DataModel
{
    public class City
    {
        public City(int id, string name, int countryId, Country country)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CountryId = countryId;
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public override string ToString()
        {
            return $"[{Id},{Name},{Country}]";
        }
    }
}
