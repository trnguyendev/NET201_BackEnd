using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên loại sản phẩm không được bỏ trống!")]
        [MaxLength(30, ErrorMessage = "Tên loại sản phẩm quá dài!")]
        [DisplayName("Tên danh mục")]
        public required string Name { get; set; }


        [Range(1, 100, ErrorMessage = "Thứ tự hiển thị phải nằm trong khoảng 1 - 100!")]
        public int DisplayOrder { get; set; }
    }
}
