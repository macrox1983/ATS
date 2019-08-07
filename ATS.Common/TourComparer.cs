using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common
{
    public class TourComparer : IComparer<Tour>
    {
        private readonly ToursOrderEnum _toursOrder;

        public TourComparer(ToursOrderEnum toursOrder)
        {
            _toursOrder = toursOrder;
        }

        public int Compare(Tour x, Tour y)
        {
            int compareResult = 0;
            switch (_toursOrder)
            {
                case ToursOrderEnum.ByPrice:
                    compareResult = x.HotelNightPriceByPerson > y.HotelNightPriceByPerson ? 1 : -1;
                    break;
                case ToursOrderEnum.ByPriceDesc:
                    compareResult = x.HotelNightPriceByPerson > y.HotelNightPriceByPerson ? -1 : 1;
                    break;

                case ToursOrderEnum.ByDate:
                    compareResult = x.DepartureDate > y.DepartureDate ? 1 : -1;
                    break;
                case ToursOrderEnum.ByDateDesc:
                    compareResult = x.DepartureDate > y.DepartureDate ? -1 : 1;
                    break;
                case ToursOrderEnum.ByName:
                    compareResult = Comparer<string>.Default.Compare(x.HotelName, y.HotelName);
                    break;

            }
            return compareResult;
        }
    }
}
