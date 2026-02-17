using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductRequest request);
        Task UpdateProductAsync(int id, CreateProductRequest request);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductHomeDto>> GetHomeProductsAsync();

    }
}
