using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }

        // --- BỔ SUNG ---
        public bool IsMain { get; set; } = false; // True: Hiển thị ở trang Home
        public int? ColorId { get; set; }         // Null: Ảnh chung, Có giá trị: Ảnh của màu cụ thể

        // Navigation property
        public Product Product { get; set; }
        public ProductColor Color { get; set; } // Nếu bạn có bảng Color riêng
    }
}
