using Microsoft.EntityFrameworkCore;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Persistence.Interceptors;
using System.Reflection;

namespace PeruGroup.Ecommerce.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Discount> Discounts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Discount>().ToTable("Discounts");
            builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
