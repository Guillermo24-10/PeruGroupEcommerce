using PeruGroup.Ecommerce.Domain.Entities;

namespace PeruGroup.Ecommerce.Application.Interface.Persistence
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<Discount> GetAsync(int id, CancellationToken cancellationToken);
        Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken);
    }
}
