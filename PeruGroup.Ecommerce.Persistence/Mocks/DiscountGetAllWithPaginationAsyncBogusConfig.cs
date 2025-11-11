using Bogus;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Domain.Enums;

namespace PeruGroup.Ecommerce.Persistence.Mocks
{
    public class DiscountGetAllWithPaginationAsyncBogusConfig : Faker<Discount>
    {
        public DiscountGetAllWithPaginationAsyncBogusConfig()
        {
            RuleFor(p=>p.Id, f => f.IndexFaker + 1);    
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
            RuleFor(p => p.Percent, f => f.Random.Decimal(70, 90));
            RuleFor(p => p.Status, f => f.PickRandom<DiscountStatus>());
        }
    }
}
