using ATS.Common.DataModel;
using System;

namespace ATS.Common.Specification
{
    public class PersonMaxCountSpecification : Specification<Tour>
    {
        private readonly int _personMaxCount;

        public PersonMaxCountSpecification(int personMaxCount)
        {
            _personMaxCount = personMaxCount>0?personMaxCount: throw new ArgumentOutOfRangeException(nameof(personMaxCount));
        }
        public override bool IsSatisfied(Tour item)
        {          
            return item.PersonMaxCount == _personMaxCount;
        }
    }
}
