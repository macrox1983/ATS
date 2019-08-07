using ATS.Common.DataModel;
using System;

namespace ATS.Common.Specification
{
    public class TourBeginDate : Specification<Tour>
    {
        private readonly DateTime _beginDate;


        public TourBeginDate(DateTime beginDate)
        {
            _beginDate = beginDate>DateTime.Now ? beginDate  : throw new ArgumentOutOfRangeException(nameof(beginDate));
        }

        public override bool IsSatisfied(Tour item)
        {
            return item.DepartureDate == _beginDate;
        }
    }
}
