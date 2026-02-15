using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //n-1 (Category - Product)
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
