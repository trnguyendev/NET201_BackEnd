using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;
using static SportStore.Application.DTOs.OrderDto;

namespace SportStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<bool> PlaceOrderAsync(CheckoutRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new Exception("Giỏ hàng trống!");

            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            // Lấy toàn bộ biến thể và sản phẩm để check (Tối ưu hơn là query trong vòng lặp)
            var allVariants = await _unitOfWork.ProductVariants.GetAllAsync();
            var allProducts = await _unitOfWork.Products.GetAllAsync();

            foreach (var item in request.Items)
            {
                // 1. Tìm biến thể trong DB
                var variant = allVariants.FirstOrDefault(v =>
                    v.ProductId == item.ProductId &&
                    v.ColorId == item.ColorId &&
                    v.SizeId == item.SizeId);

                if (variant == null)
                    throw new Exception($"Sản phẩm ID {item.ProductId} không tồn tại mẫu mã này!");

                // 2. Check tồn kho
                if (variant.Quantity < item.Quantity)
                    throw new Exception($"Sản phẩm ID {item.ProductId} chỉ còn {variant.Quantity} cái, không đủ số lượng yêu cầu!");

                // 3. Trừ kho trực tiếp
                variant.Quantity -= item.Quantity;
                _unitOfWork.ProductVariants.Update(variant);

                // 4. Lấy giá chuẩn từ Database để tính tiền
                var product = allProducts.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null || !product.IsActive)
                    throw new Exception($"Sản phẩm ID {item.ProductId} đã ngừng kinh doanh!");

                totalAmount += product.BasePrice * item.Quantity;

                // 5. Thêm vào chi tiết đơn
                orderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ColorId = item.ColorId,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity,
                    UnitPrice = product.BasePrice // Lưu cứng giá lúc mua
                });
            }

            // 6. Tạo đơn hàng mới
            var order = new Order
            {
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                ShippingAddress = request.ShippingAddress,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = totalAmount,
                OrderDetails = orderDetails,
                Status = 0, // Chờ xác nhận
                OrderDate = DateTime.Now
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync(); // Lưu tất cả (Trừ kho + Tạo đơn) vào DB cùng lúc

            return true;
        }
    }
}
