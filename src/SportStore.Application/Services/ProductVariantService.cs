using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductVariantService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IEnumerable<ProductVariantDto>> GetVariantsByProductIdAsync(int productId)
        {
            // Lấy tất cả và lọc theo ProductId (Lưu ý: Nếu UnitOfWork của bạn có hàm Find(x => x.ProductId == productId) thì dùng sẽ tối ưu hơn)
            var allVariants = await _unitOfWork.ProductVariants.GetAllAsync();
            var variants = allVariants.Where(v => v.ProductId == productId).ToList();

            // Để lấy được Tên Màu và Tên Size, ta lấy list Color/Size ra map (hoặc dùng Include nếu Repo hỗ trợ)
            var colors = await _unitOfWork.ProductColors.GetAllAsync(); // Đảm bảo bạn có Repo cho ProductColors
            var sizes = await _unitOfWork.ProductSizes.GetAllAsync();   // Đảm bảo bạn có Repo cho ProductSizes

            return variants.Select(v => new ProductVariantDto
            {
                Id = v.Id,
                ProductId = v.ProductId,
                ColorId = v.ColorId,
                ColorName = colors.FirstOrDefault(c => c.Id == v.ColorId)?.Name,
                SizeId = v.SizeId,
                SizeName = sizes.FirstOrDefault(s => s.Id == v.SizeId)?.Name,
                Quantity = v.Quantity,
                SkuCode = v.SkuCode
            });
        }

        public async Task<ProductVariantDto> CreateVariantAsync(CreateProductVariantRequest request)
        {
            // Kiểm tra xem tổ hợp ProductId + ColorId + SizeId đã tồn tại chưa
            var allVariants = await _unitOfWork.ProductVariants.GetAllAsync();
            bool exists = allVariants.Any(v => v.ProductId == request.ProductId
                                            && v.ColorId == request.ColorId
                                            && v.SizeId == request.SizeId);
            if (exists)
            {
                throw new Exception("Biến thể (Màu + Size) này đã tồn tại cho sản phẩm!");
            }

            var variant = new ProductVariant
            {
                ProductId = request.ProductId,
                ColorId = request.ColorId,
                SizeId = request.SizeId,
                Quantity = request.Quantity,
                SkuCode = request.SkuCode
            };

            await _unitOfWork.ProductVariants.AddAsync(variant);
            await _unitOfWork.CompleteAsync();

            return new ProductVariantDto { Id = variant.Id, ProductId = variant.ProductId };
        }

        public async Task DeleteVariantAsync(int id)
        {
            var variant = await _unitOfWork.ProductVariants.GetByIdAsync(id);
            if (variant != null)
            {
                _unitOfWork.ProductVariants.Delete(variant);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task UpdateVariantAsync(int id, UpdateProductVariantRequest request)
        {
            var variant = await _unitOfWork.ProductVariants.GetByIdAsync(id);
            if (variant != null)
            {
                variant.Quantity = request.Quantity;
                variant.SkuCode = request.SkuCode;

                _unitOfWork.ProductVariants.Update(variant);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
