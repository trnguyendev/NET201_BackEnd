using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    // Dữ liệu client gửi lên để tạo mới
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống!")]
        [MaxLength(200, ErrorMessage = "Tên sản phẩm không quá 200 ký tự")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(1000, 100000000, ErrorMessage = "Giá phải nằm trong khoảng 1k đến 100tr")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Mô tả không được bỏ trống!")]
        [MaxLength(1000, ErrorMessage = "Mô tả sản phẩm quá dài")]
        public required string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn loại sản phẩm hợp lệ!")]
        public int CategoryId { get; set; }
    }


    // Dữ liệu trả về cho client
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
