using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<CategoriesDto>>> GetAll();
    }
}
