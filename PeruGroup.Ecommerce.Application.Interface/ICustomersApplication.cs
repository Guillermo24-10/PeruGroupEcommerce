using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface ICustomersApplication
    {
        Task<Response<bool>> InsertAsync(CutomersDto cutomersDto);
        Task<Response<bool>> UpdateAsync(CutomersDto cutomersDto);
        Task<Response<bool>> DeleteAsync(string id);
        Task<Response<CutomersDto>> GetByIdAsync(string id);
        Task<Response<IEnumerable<CutomersDto>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CutomersDto>>> GetAllPaginationAsync(int pageNumber, int pageSize);
    }
}
