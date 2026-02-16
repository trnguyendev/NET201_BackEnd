using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Domain.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }

        // Liên kết với Sản phẩm cha
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        // Liên kết với Màu
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public virtual ProductColor? Color { get; set; }

        // Liên kết với Size
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual ProductSize? Size { get; set; }

        // Số lượng tồn kho
        public int Quantity { get; set; }

        // Mã SKU (để quản lý kho dễ hơn)
        public string? SkuCode { get; set; }
    }
}
