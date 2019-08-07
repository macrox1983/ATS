using System.Linq;

namespace ATS.Common.Specification
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params Specification<T>[] specifications):base(specifications)
        {

        }

        public override bool IsSatisfied(T item)
        {
            return _specifications.All(s=>s.IsSatisfied(item));
        }
    }
}
