using Microsoft.EntityFrameworkCore;
using PeruGroup.Ecommerce.Application.Interface.Persistence;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Persistence.Contexts;
using PeruGroup.Ecommerce.Persistence.Mocks;

namespace PeruGroup.Ecommerce.Persistence.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() => 1000);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<Discount>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)));

            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            _context.Remove(entity);
            return await Task.FromResult(true);

        }

        public async Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<Discount>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<Discount>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Discount>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var faker = new DiscountGetAllWithPaginationAsyncBogusConfig();
            var result = await Task.Run(() => faker.Generate(1000));
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public async Task<Discount> GetAsync(int id, CancellationToken cancellationToken)
        {
            return (await _context.Set<Discount>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken))!;
        }

        public Task<Discount> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Discount discount)
        {
            _context.Add(discount);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Discount discount)
        {
            var entity = await _context.Set<Discount>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(discount.Id));
            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            entity.Name = discount.Name;
            entity.Description = discount.Description;
            entity.Percent = discount.Percent;
            entity.Status = discount.Status;

            _context.Update(entity);

            return await Task.FromResult(true);
        }
    }
}
