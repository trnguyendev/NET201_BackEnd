using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ProductColor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên màu không được bỏ trống!")]
        [MaxLength(50)]
        [DisplayName("Tên màu")]
        public required string Name { get; set; }

        [MaxLength(7)]
        public string? HexCode { get; set; }
    }
}
