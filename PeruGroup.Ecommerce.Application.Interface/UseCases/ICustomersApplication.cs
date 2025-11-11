using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface.UseCases
{
    public interface ICustomersApplication
    {
        Task<Response<bool>> InsertAsync(CustomerDto cutomersDto);
        Task<Response<bool>> UpdateAsync(CustomerDto cutomersDto);
        Task<Response<bool>> DeleteAsync(string id);
        Task<Response<CustomerDto>> GetByIdAsync(string id);
        Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllPaginationAsync(int pageNumber, int pageSize);
    }
}
