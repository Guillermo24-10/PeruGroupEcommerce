using Dapper;
using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Infrastructure.Data;
using PeruGroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace PeruGroup.Ecommerce.Infrastructure.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext _context;

        public CategoriesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Categories";

            var response = await connection.QueryAsync<Categories>(query, commandType: CommandType.Text);

            return response;
        }
    }
}
