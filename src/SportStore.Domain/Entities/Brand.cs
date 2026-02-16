namespace SportStore.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? LogoUrl { get; set; }
    }
}
