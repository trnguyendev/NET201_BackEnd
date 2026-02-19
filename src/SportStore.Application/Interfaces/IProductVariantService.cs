using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<IEnumerable<ProductVariantDto>> GetVariantsByProductIdAsync(int productId);
        Task<ProductVariantDto> CreateVariantAsync(CreateProductVariantRequest request); // Đổi tên param thành CreateProductVariantRequest cho chuẩn nhé, ở dưới mình viết là CreateProductVariantRequest
        Task DeleteVariantAsync(int id);
        Task UpdateVariantAsync(int id, UpdateProductVariantRequest request);
    }
}
