using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.DTOs
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public string? Thumbnail { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }

        // Chứa luôn danh sách biến thể để Frontend dễ xử lý
        public List<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();

        // Tương lai nếu có bảng ProductImages, bạn sẽ thêm List<string> Images vào đây
    }
}
