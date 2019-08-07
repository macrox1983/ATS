using System;
using ATS.Common.DataModel;

namespace ATS.Common.Specification
{
    public class HotelNightCountSpecification : Specification<Tour>
    {
        private readonly int _nightCountBegin;

        private readonly int _nightCountEnd;

        public HotelNightCountSpecification(int nightCountFrom, int nightCountTo)
        {
            if (_nightCountBegin > nightCountTo)
                throw new ArgumentException("Начало диапазона должно быть меньше конца");

            _nightCountBegin = nightCountFrom > 0? nightCountFrom : throw new ArgumentOutOfRangeException(nameof(nightCountFrom));

            _nightCountEnd = nightCountTo > 0? nightCountTo : throw new ArgumentOutOfRangeException(nameof(nightCountTo));
        }

        public override bool IsSatisfied(Tour item)
        {
            return item.HotelNightCount >= _nightCountBegin && item.HotelNightCount<=_nightCountEnd;
        }
    }
}
