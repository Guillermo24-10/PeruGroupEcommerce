using PeruGroup.Ecommerce.Application.Interface.Persistence;

namespace PeruGroup.Ecommerce.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository CustomersRepository { get; }
        IUsersRepository UsersRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        IDiscountRepository DiscountRepository { get; }

        Task<int> Save(CancellationToken cancellationToken);
    }
}
