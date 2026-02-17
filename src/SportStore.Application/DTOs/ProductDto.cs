using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    // Dữ liệu trả về cho client
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Dữ liệu client gửi lên để tạo mới
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống!")]
        [MaxLength(200, ErrorMessage = "Tên sản phẩm không quá 200 ký tự")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(1000, 100000000, ErrorMessage = "Giá phải nằm trong khoảng 1k đến 100tr")]
        public decimal Price { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả sản phẩm quá dài")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm!")]
        [Range(1, int.MaxValue, ErrorMessage = "Danh mục không hợp lệ!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thương hiệu!")]
        [Range(1, int.MaxValue, ErrorMessage = "Thương hiệu không hợp lệ!")]
        public int BrandId { get; set; }

        public string? Thumbnail { get; set; }

        public IFormFile? MainImage { get; set; } // Ảnh đại diện bắt buộc
        public List<ProductVariantImageDto>? VariantImages { get; set; } = null;

    }

    public class ProductVariantImageDto
    {
        public IFormFile ImageFile { get; set; }
        public int ColorId { get; set; } // User chọn màu cho ảnh này
    }
}
