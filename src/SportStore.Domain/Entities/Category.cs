namespace SportStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }
}
