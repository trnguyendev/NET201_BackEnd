namespace SportStore.Domain.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
