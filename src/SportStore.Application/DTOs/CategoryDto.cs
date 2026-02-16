using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{

    // Dữ liệu trả về cho client
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Tên loại sản phẩm không được bỏ trống!")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được quá 50 ký tự!")]
        public required string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Thứ tự hiển thị phải nằm trong khoảng 1 - 100!")]
        public int DisplayOrder { get; set; }
    }

    public class UpdateCategoryRequest
    {
        [Required(ErrorMessage = "Tên loại sản phẩm không được bỏ trống!")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được quá 50 ký tự!")]
        public required string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Thứ tự hiển thị phải nằm trong khoảng 1 - 100!")]
        public int DisplayOrder { get; set; }
    }
}
