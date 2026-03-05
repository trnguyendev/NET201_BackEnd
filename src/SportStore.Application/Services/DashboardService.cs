using Microsoft.AspNetCore.Identity; // Thêm nếu bạn dùng UserManager cho User
using Microsoft.EntityFrameworkCore;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        // (Tùy chọn) Nếu bạn quản lý User bằng Identity, hãy Inject UserManager vào đây 
        // để đếm số lượng khách hàng. Nếu dùng UnitOfWork thì bỏ qua UserManager.
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var summary = new DashboardSummaryDto();

            // 1. Đếm tổng số sản phẩm
            summary.TotalProducts = await _unitOfWork.Products.GetQueryable().CountAsync();

            // 2. Thống kê Đơn hàng & Tổng doanh thu
            var allOrders = await _unitOfWork.Orders.GetQueryable().ToListAsync();

            summary.TotalOrders = allOrders.Count;

            // 👉 Chỉ cộng tiền các đơn hàng có Status == 2 (Hoàn thành)
            summary.TotalRevenue = allOrders
                .Where(o => o.Status == 2)
                .Sum(o => o.TotalAmount);

            // 3. Đếm số lượng khách hàng
            // Lấy danh sách những User thuộc Role "Customer"
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            summary.TotalCustomers = customers.Count;

            // 4. Lấy 5 đơn hàng mới nhất
            // Bước A: Lấy dữ liệu thô từ Database (Lấy 5 đơn xếp theo ngày mới nhất)
            var recentOrdersEntities = await _unitOfWork.Orders.GetQueryable()
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            // Bước B: Chuyển đổi (Map) dữ liệu từ Entity sang DTO cho Frontend
            summary.RecentOrders = recentOrdersEntities.Select(o => new RecentOrderDto
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = GetStatusName(o.Status) // 👉 Gọi hàm dịch trạng thái ở dưới
            }).ToList();

            return summary;
        }

        // --- Hàm hỗ trợ dịch Trạng thái từ SỐ (int) sang CHỮ (string) ---
        private string GetStatusName(int statusValue)
        {
            return statusValue switch
            {
                0 => "Chờ xác nhận",
                1 => "Đang giao",
                2 => "Hoàn thành",
                3 => "Đã hủy",
                _ => "Không xác định"
            };
        }
    }
}