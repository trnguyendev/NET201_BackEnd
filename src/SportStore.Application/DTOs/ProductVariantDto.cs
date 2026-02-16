using SportStore.Domain.Entities;

namespace SportStore.Application.DTOs
{
    public class ProductVariantDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int ColorId { get; set; }

        public virtual ProductColor? Color { get; set; }

        public int SizeId { get; set; }
        public virtual ProductSize? Size { get; set; }

        public int Quantity { get; set; }

        public string? SkuCode { get; set; }
    }

    public class CreateProductVariantRequest
    {
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public int ColorId { get; set; }

        public virtual ProductColor? Color { get; set; }

        public int SizeId { get; set; }

        public virtual ProductSize? Size { get; set; }

        public int Quantity { get; set; }

        public string? SkuCode { get; set; }
    }

    public class UpdateProductVariantRequest
    {
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public int ColorId { get; set; }

        public virtual ProductColor? Color { get; set; }

        public int SizeId { get; set; }

        public virtual ProductSize? Size { get; set; }

        public int Quantity { get; set; }

        public string? SkuCode { get; set; }
    }
}
