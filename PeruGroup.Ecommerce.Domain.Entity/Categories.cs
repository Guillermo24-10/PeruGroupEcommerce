namespace PeruGroup.Ecommerce.Domain.Entity
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } 
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
    }
}
