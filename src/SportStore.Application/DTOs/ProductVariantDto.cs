using SportStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string? ColorName { get; set; } // Trả về tên màu để FE hiển thị
        public int SizeId { get; set; }
        public string? SizeName { get; set; }   // Trả về tên size để FE hiển thị
        public int Quantity { get; set; }
        public string? SkuCode { get; set; }
    }

    public class CreateProductVariantRequest
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Màu sắc")]
        public int ColorId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Kích thước")]
        public int SizeId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        public int Quantity { get; set; }
        public string? SkuCode { get; set; }
    }


    public class UpdateProductVariantRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        public int Quantity { get; set; }
        public string? SkuCode { get; set; }
    }
}
