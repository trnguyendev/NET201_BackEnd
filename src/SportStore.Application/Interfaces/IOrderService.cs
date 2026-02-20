using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportStore.Application.DTOs.OrderDto;

namespace SportStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(CheckoutRequest request);
    }
}
