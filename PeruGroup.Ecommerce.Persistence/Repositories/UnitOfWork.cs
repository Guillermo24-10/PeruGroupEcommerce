using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Interface.Persistence;
using PeruGroup.Ecommerce.Persistence.Contexts;

namespace PeruGroup.Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ICustomersRepository customersRepository, IUsersRepository usersRepository, ICategoriesRepository categoriesRepository, IDiscountRepository discountRepository, ApplicationDbContext context)
        {
            CustomersRepository = customersRepository;
            UsersRepository = usersRepository;
            CategoriesRepository = categoriesRepository;
            DiscountRepository = discountRepository;
            _context = context;
        }

        public ICustomersRepository CustomersRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public ICategoriesRepository CategoriesRepository { get; }

        public IDiscountRepository DiscountRepository { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
