using PeruGroup.Ecommerce.Infrastructure.Interface;

namespace PeruGroup.Ecommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICustomersRepository customersRepository, IUsersRepository usersRepository, ICategoriesRepository categoriesRepository)
        {
            CustomersRepository = customersRepository;
            UsersRepository = usersRepository;
            CategoriesRepository = categoriesRepository;
        }

        public ICustomersRepository CustomersRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public ICategoriesRepository CategoriesRepository { get; }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
