using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.DTOs
{
    public class BrandDto
    {
        public required string Name { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class CreateBrandRequest
    {
        [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống!")]
        public required string Name { get; set; }

        public string? LogoUrl { get; set; }
    }

    public class UpdateBrandRequest
    {
        [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống!")]
        public required string Name { get; set; }

        public string? LogoUrl { get; set; }

    }
}
