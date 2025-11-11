using Dapper;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Persistence.Contexts;
using System.Data;

namespace PeruGroup.Ecommerce.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;

        public UsersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);

                var user = await conn.QuerySingleOrDefaultAsync<User>(query, parameters, commandType: CommandType.StoredProcedure);

                return user!;
            }
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(User customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User customer)
        {
            throw new NotImplementedException();
        }
    }
}
