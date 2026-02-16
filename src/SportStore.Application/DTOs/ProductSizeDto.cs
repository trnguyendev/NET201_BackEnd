using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    public class ProductSizeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Type { get; set; }
    }

    public class CreateProductSizeRequest
    {
        [Required(ErrorMessage = "Tên size không được bỏ trống!")]
        [MaxLength(10, ErrorMessage = "Tên size quá dài!")]
        public required string Name { get; set; }

        public string? Type { get; set; }
    }

    public class UpdateProductSizeRequest
    {
        [Required(ErrorMessage = "Tên size không được bỏ trống!")]
        [MaxLength(10, ErrorMessage = "Tên size quá dài!")]
        public required string Name { get; set; }

        public string? Type { get; set; }
    }
}
