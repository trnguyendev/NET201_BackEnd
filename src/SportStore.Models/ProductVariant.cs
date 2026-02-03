using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        // Liên kết với Sản phẩm cha
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        // Liên kết với Màu
        [Required(ErrorMessage = "Phải chọn màu sắc")]
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public virtual ProductColor? Color { get; set; }

        // Liên kết với Size
        [Required(ErrorMessage = "Phải chọn kích thước")]
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual ProductSize? Size { get; set; }

        // Số lượng tồn kho
        [Required]
        [Range(0, 10000, ErrorMessage = "Số lượng tồn kho không hợp lệ")]
        [DisplayName("Số lượng tồn kho")]
        public int Quantity { get; set; }

        // Mã SKU (để quản lý kho dễ hơn)
        [MaxLength(50)]
        public string? SkuCode { get; set; }
    }
}
