using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.DTOs
{
    public class ProductHomeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public string? Thumbnail { get; set; }
        public string? CategoryName { get; set; } // Hiển thị thêm tên danh mục cho đẹp
        public string? BrandName { get; set; }
    }
}
