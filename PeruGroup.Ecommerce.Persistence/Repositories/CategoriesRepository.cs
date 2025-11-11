using Dapper;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Persistence.Contexts;
using System.Data;

namespace PeruGroup.Ecommerce.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext _context;

        public CategoriesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Categories";

            var response = await connection.QueryAsync<Category>(query, commandType: CommandType.Text);

            return response;
        }
    }
}
