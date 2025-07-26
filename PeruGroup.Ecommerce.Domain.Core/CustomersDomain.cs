using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Infrastructure.Interface;

namespace PeruGroup.Ecommerce.Domain.Core
{
    public class CustomersDomain : ICustomersDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> CountAsync()
        {
            return _unitOfWork.CustomersRepository.CountAsync();
        }

        public Task<bool> DeleteAsync(string id)
        {
            return _unitOfWork.CustomersRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Customers>> GetAllAsync()
        {
            return _unitOfWork.CustomersRepository.GetAllAsync();
        }

        public Task<IEnumerable<Customers>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            return _unitOfWork.CustomersRepository.GetAllWithPaginationAsync(pageNumber, pageSize);
        }

        public Task<Customers> GetByIdAsync(string id)
        {
            return _unitOfWork.CustomersRepository.GetByIdAsync(id);
        }

        public Task<bool> InsertAsync(Customers customer)
        {
            return _unitOfWork.CustomersRepository.InsertAsync(customer);
        }

        public Task<bool> UpdateAsync(Customers customer)
        {
            return _unitOfWork.CustomersRepository.UpdateAsync(customer);
        }
    }
}
