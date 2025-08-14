using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Domain.Interface
{
    public interface ICategoriesDomain
    {
        Task<IEnumerable<Categories>> GetAll();
    }
}
