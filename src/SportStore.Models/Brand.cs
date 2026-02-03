using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Tên thương hiệu không được quá 100 ký tự!")]
        public required string Name { get; set; }

        public string? LogoUrl { get; set; } 
    }
}
