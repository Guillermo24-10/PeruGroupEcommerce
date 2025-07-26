using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Domain.Interface
{
    public interface ICustomersDomain
    {
        Task<bool> InsertAsync(Customers customer);
        Task<bool> UpdateAsync(Customers customer);
        Task<bool> DeleteAsync(string id);
        Task<Customers> GetByIdAsync(string id);
        Task<IEnumerable<Customers>> GetAllAsync();
        Task<IEnumerable<Customers>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
    }
}
