using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface.UseCases
{
    public interface IDiscountApplication
    {
        Task<Response<bool>> Create(DiscountDto discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(DiscountDto discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDto>> Get(int id, CancellationToken cancellationToken = default);
        Task<Response<List<DiscountDto>>> GetAll(CancellationToken cancellationToken = default);
        Task<ResponsePagination<IEnumerable<DiscountDto>>> GetAllWithPagination(int pageNumber, int pageSize);
    }
}
