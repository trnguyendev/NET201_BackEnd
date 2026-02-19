using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Application.DTOs
{
    public class BrandDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class CreateBrandRequest
    {
        [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống!")]
        public required string Name { get; set; }

        public IFormFile? LogoFile { get; set; }
    }

    public class UpdateBrandRequest
    {
        [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống!")]
        public required string Name { get; set; }

        public IFormFile? LogoFile { get; set; }

    }
}
