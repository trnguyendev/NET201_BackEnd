using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductSizeService
    {
        public Task<IEnumerable<ProductSizeDto>> GetAllProductSizesAsync();
        public Task<ProductSizeDto> GetProductByIdAsync(int id);
        public Task<ProductSizeDto> CreateProductSizeAsync(CreateProductSizeRequest request);
        public Task UpdateProductSizeAsync(int id, UpdateProductSizeRequest request);
        public Task DeleteProductSizeAsync(int id);
    }
}
