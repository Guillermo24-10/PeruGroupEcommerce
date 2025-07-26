using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Domain.Interface
{
    public interface IUsersDomain
    {
        Task<Users> Authenticate(string username, string password);
    }
}
