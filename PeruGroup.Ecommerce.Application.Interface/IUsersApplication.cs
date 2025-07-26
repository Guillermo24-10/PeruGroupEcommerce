using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface IUsersApplication
    {
        Task<Response<UsersDTO>> Authenticate(string username, string password);
    }
}
