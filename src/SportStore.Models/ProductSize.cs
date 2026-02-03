using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên size không được bỏ trống!")]
        [MaxLength(10, ErrorMessage = "Tên size quá dài!")]
        public required string Name { get; set; }

        public string? Type { get; set; } 
    }
}
