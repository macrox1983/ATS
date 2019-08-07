using System.Text;

namespace ATS.Common.Specification
{

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T item);

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }       
    }
}
