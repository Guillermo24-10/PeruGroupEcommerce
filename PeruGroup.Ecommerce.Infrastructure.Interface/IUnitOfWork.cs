namespace PeruGroup.Ecommerce.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository CustomersRepository { get; }
        IUsersRepository UsersRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
    }
}
