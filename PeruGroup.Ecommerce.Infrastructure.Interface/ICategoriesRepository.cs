using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Infrastructure.Interface
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAll();
    }
}
