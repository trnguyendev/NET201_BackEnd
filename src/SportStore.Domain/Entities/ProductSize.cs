namespace SportStore.Domain.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Type { get; set; }
    }
}
