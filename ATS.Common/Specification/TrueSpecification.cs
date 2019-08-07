
using ATS.Common.DataModel;

namespace ATS.Common.Specification
{
    public class TrueSpecification : Specification<Tour>
    {
        public override bool IsSatisfied(Tour item)
        {
            return true;
        }
    }
}
