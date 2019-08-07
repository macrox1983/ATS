using ATS.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ATS.Common.DataModel;
using ATS.Common.Specification;

namespace ATS.Common.Specification
{
    /// <summary>
    /// Класс хелпер для разворачивания шаблона поиска в спецификацию Tour
    /// </summary>
    public class SpecificationHelper
    {
        /// <summary>
        /// Получаем спецификацию на основе шаблона поиска
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Specification<Tour> GetSpecification(SearchPattern pattern)
        {
            Specification<Tour> specification = new TrueSpecification();

            // здесь и далее - проверяем, если пришло заполненное поле по которому ищем,
            // добавляем спецификацию к уже имеющимся
            if (!string.IsNullOrWhiteSpace(pattern.DepartureCity))
                specification &= new DepartureCitySpecification(pattern.DepartureCity);

            if (pattern.DepartureDate.HasValue)
                specification &= new TourBeginDate(pattern.DepartureDate.Value);

            if(pattern.HotelNightCountFrom.HasValue && pattern.HotelNightCountTo.HasValue)
                specification &= new HotelNightCountSpecification(pattern.HotelNightCountFrom.Value, pattern.HotelNightCountTo.Value);

            if (pattern.PersonCount.HasValue)
                specification &= new PersonMaxCountSpecification(pattern.PersonCount.Value);

            if (!string.IsNullOrWhiteSpace(pattern.TourCity))
                specification &= new TourCitySpecification(pattern.TourCity);

            return specification;
        }
    }
}
