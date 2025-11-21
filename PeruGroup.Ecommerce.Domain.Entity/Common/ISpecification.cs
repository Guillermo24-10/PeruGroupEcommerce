namespace PeruGroup.Ecommerce.Domain.Common
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
