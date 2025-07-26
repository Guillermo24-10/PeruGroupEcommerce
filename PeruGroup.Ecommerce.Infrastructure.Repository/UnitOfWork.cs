using PeruGroup.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeruGroup.Ecommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICustomersRepository customersRepository, IUsersRepository usersRepository)
        {
            CustomersRepository = customersRepository;
            UsersRepository = usersRepository;
        }

        public ICustomersRepository CustomersRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
