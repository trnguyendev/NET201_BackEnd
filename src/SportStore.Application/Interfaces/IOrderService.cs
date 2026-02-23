using SportStore.Domain.Entities;
using static SportStore.Application.DTOs.OrderDto;

namespace SportStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(CheckoutRequest request, string userId = null);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, int status);
        Task<bool> CancelUserOrderAsync(int orderId, string userId);
    }
}
