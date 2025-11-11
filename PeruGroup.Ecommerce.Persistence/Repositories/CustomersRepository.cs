using Dapper;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Persistence.Contexts;
using System.Data;

namespace PeruGroup.Ecommerce.Persistence.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DapperContext _context;

        public CustomersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "Select Count(*) from Customers";
                return await conn.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", id);
                var result = await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersList";

                var customers = await conn.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);
                return customers ?? new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersListWithPagination";
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
                return await conn.QueryAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", id);

                var customer = await conn.QuerySingleAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
                return customer ?? new Customer();
            }
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var conn = _context.CreateConnection())
            {
                var query = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
