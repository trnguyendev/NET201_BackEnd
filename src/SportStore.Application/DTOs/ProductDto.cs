using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public decimal BasePrice { get; set; }
        public string? Thumbnail { get; set; } // URL ảnh để trả về React
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        // Có thể thêm tên Category/Brand để hiển thị ra bảng cho đẹp
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
    }

    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal BasePrice { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        // Nhận file từ React gửi lên
        public IFormFile? ThumbnailFile { get; set; }
    }

    // Nên tạo UpdateRequest riêng, phòng trường hợp sau này update khác create
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public decimal BasePrice { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public IFormFile? ThumbnailFile { get; set; }
    }

    public class ProductVariantImageDto
    {
        public IFormFile ImageFile { get; set; }
        public int ColorId { get; set; } // User chọn màu cho ảnh này
    }
}
