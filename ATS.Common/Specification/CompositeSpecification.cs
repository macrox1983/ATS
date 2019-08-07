using System.Collections.Generic;

namespace ATS.Common.Specification
{
    public abstract class CompositeSpecification<T> : Specification<T>
    {
        protected readonly List<Specification<T>> _specifications;

        public CompositeSpecification(params Specification<T>[] specifications)
        {
            _specifications = new List<Specification<T>>(specifications);
        }        
    }
}
