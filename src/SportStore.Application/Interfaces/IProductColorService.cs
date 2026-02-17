using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductColorService
    {
        public Task<IEnumerable<ProductColorDto>> GetAllProductColorsAsync();
        public Task<ProductColorDto> GetProductColorByIdAsync(int id);
        public Task<ProductColorDto> CreateProductColorAsync(CreateProductColorRequest request);
        public Task UpdateProductColorAsync(int id, UpdateProductColorRequest request);
        public Task DeleteProductColorAsync(int id);
    }
}
