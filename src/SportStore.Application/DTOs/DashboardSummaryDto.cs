namespace SportStore.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public decimal TotalRevenue { get; set; } // Tổng doanh thu
        public int TotalOrders { get; set; }      // Tổng số đơn hàng
        public int TotalProducts { get; set; }    // Tổng số sản phẩm
        public int TotalCustomers { get; set; }   // Tổng số khách hàng

        // Danh sách 5 đơn hàng mới nhất
        public List<RecentOrderDto> RecentOrders { get; set; } = new List<RecentOrderDto>();
    }

    public class RecentOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}