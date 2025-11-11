using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Task<Response<UserDto>> Authenticate(string username, string password);
    }
}
