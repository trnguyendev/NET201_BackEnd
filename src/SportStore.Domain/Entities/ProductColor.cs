namespace SportStore.Domain.Entities
{
    public class ProductColor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? HexCode { get; set; }
    }
}
