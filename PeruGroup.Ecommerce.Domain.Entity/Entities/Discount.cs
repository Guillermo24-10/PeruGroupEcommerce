using PeruGroup.Ecommerce.Domain.Common;
using PeruGroup.Ecommerce.Domain.Enums;

namespace PeruGroup.Ecommerce.Domain.Entities
{
    public class Discount : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
