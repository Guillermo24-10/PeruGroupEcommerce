using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Infrastructure.Interface;

namespace PeruGroup.Ecommerce.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Users> Authenticate(string username, string password)
        {
            return _unitOfWork.UsersRepository.Authenticate(username, password);
        }
    }
}
