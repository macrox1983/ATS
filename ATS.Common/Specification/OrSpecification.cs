using System.Linq;

namespace ATS.Common.Specification
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(params Specification<T>[] specifications) : base(specifications)
        {

        }

        public override bool IsSatisfied(T item)
        {
            return _specifications.Any(s=>s.IsSatisfied(item));
        }
    }
}
