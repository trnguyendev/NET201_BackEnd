using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? HexCode { get; set; }
    }

    public class CreateProductColorRequest
    {
        [Required(ErrorMessage = "Tên màu không được bỏ trống!")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(7, ErrorMessage = "Mã màu quá dài!")]
        public string? HexCode { get; set; }
    }

    public class UpdateProductColorRequest
    {
        [Required(ErrorMessage = "Tên màu không được bỏ trống!")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(7, ErrorMessage = "Mã màu quá dài!")]
        public string? HexCode { get; set; }
    }
}
