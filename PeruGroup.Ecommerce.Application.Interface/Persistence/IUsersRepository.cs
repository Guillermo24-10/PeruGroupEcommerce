using PeruGroup.Ecommerce.Domain.Entities;

namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(string username, string password);
    }
}
