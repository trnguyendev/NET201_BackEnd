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
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống!")]
        [MaxLength(200, ErrorMessage = "Tên sản phẩm không quá 200 ký tự")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(1000, 100000000, ErrorMessage = "Giá phải nằm trong khoảng 1k đến 100tr")]
        [Column(TypeName = "decimal(18,2)")] // Định dạng tiền tệ trong SQL
        public decimal BasePrice { get; set; }

        public string? Thumbnail { get; set; }

        // --- Khóa ngoại (Foreign Keys) ---
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }
    }
}
