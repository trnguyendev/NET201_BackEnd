namespace SportStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Thông tin khách mua
        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ShippingAddress { get; set; } = null!;

        // Thông tin thanh toán
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = "COD"; // COD, VNPay...

        // Trạng thái đơn: 0-Chờ xác nhận, 1-Đang giao, 2-Hoàn thành, 3-Đã hủy
        public int Status { get; set; } = 0;

        // Navigation (1 đơn hàng có nhiều chi tiết)
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
