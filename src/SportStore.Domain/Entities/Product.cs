using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        // 1. Thumbnail: Lưu đường dẫn ảnh đại diện (Dạng chuỗi là ĐÚNG)
        // Mục đích: Hiển thị nhanh ở trang danh sách mà không cần join bảng
        public string? Thumbnail { get; set; }

        // 2. ProductImages: Phải là DANH SÁCH (Quan hệ 1-Nhiều)
        // SAI: public string? ProductImages { get; set; } 
        // SỬA THÀNH:
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        // Foreign keys
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        // Navigation
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}