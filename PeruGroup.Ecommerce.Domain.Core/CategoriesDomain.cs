using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Infrastructure.Interface;

namespace PeruGroup.Ecommerce.Domain.Core
{
    public class CategoriesDomain : ICategoriesDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Categories>> GetAll()
        {
            return _unitOfWork.CategoriesRepository.GetAll();
        }
    }
}
