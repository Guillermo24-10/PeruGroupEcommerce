namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> InsertAsync(T customer);
        Task<bool> UpdateAsync(T customer);
        Task<bool> DeleteAsync(string id);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
    }
}
