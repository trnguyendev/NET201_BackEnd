using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.DTOs
{
    public class OrderDto
    {
        public class CheckoutRequest
        {
            [Required(ErrorMessage = "Vui lòng nhập họ tên")]
            public string CustomerName { get; set; } = null!;

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
            public string PhoneNumber { get; set; } = null!;

            [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
            public string ShippingAddress { get; set; } = null!;

            public string PaymentMethod { get; set; } = "COD";

            // Danh sách các món hàng truyền từ LocalStorage lên
            public List<CartItemRequest> Items { get; set; } = new List<CartItemRequest>();
        }

        public class CartItemRequest
        {
            public int ProductId { get; set; }
            public int ColorId { get; set; }
            public int SizeId { get; set; }
            public int Quantity { get; set; }
            // Lưu ý: Không nhận "Giá" từ Frontend gửi lên để tránh khách hàng hack đổi giá. 
            // Chúng ta sẽ lấy giá gốc từ Database ở bước Service!
        }
    }
}
