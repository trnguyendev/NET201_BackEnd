using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductVariantService
    {
        public Task<IEnumerable<ProductVariantDto>> GetAllProductVariantsAsync();
        public Task<ProductVariantDto> GellProductVariantByIdAsnyc(int id);
        public Task<ProductVariantDto> CreateProductVarianAsync(CreateProductVariantRequest request);
        public Task UpdateProductVarianAsync(int id, UpdateProductVariantRequest request);
        public Task DeleteProductVariantAsync(int id);
    }
}
