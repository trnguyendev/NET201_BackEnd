using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Interfaces;
using System.Security.Claims;
using static SportStore.Application.DTOs.OrderDto;

namespace SportStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) { _orderService = orderService; }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, status);
            if (!result) return NotFound();
            return Ok(new { message = "Cập nhật trạng thái thành công" });
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            try
            {
                // 👉 Lấy userId từ JWT Token. 
                // Nếu user chưa đăng nhập, userId sẽ = null.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Truyền userId xuống Service
                await _orderService.PlaceOrderAsync(request, userId);

                return Ok(new { message = "Đặt hàng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("my-orders")]
        [Authorize] // Bắt buộc đăng nhập
        public async Task<IActionResult> GetMyOrders()
        {
            // Lấy UserId từ JWT Token một cách an toàn
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpPut("my-orders/{id}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelMyOrder(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                await _orderService.CancelUserOrderAsync(id, userId);
                return Ok(new { message = "Đã hủy đơn hàng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
