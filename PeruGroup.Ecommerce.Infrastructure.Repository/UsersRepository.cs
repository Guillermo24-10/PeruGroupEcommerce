using Dapper;
using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Infrastructure.Data;
using PeruGroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace PeruGroup.Ecommerce.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;

        public UsersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Users> Authenticate(string username, string password)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);

                var user = await conn.QuerySingleAsync<Users>(query, parameters, commandType: CommandType.StoredProcedure);

                return user;
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

        public Task<IEnumerable<Users>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Users customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Users customer)
        {
            throw new NotImplementedException();
        }
    }
}
