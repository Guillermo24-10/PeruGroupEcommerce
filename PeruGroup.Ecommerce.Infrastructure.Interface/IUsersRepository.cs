using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Infrastructure.Interface
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<Users> Authenticate(string username, string password);
    }
}
