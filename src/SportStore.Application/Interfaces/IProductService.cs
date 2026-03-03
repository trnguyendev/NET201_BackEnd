using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<PageResult<ProductDto>> GetAllProductsAsync(int pageNumber = 1, int pageSize = 20);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductRequest request);
        Task UpdateProductAsync(int id, UpdateProductRequest request);
        Task DeleteProductAsync(int id);
        Task<PageResult<ProductHomeDto>> GetHomeProductsAsync(int pageNumber, int pageSize);
        Task<PageResult<ProductHomeDto>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
        Task<ProductDetailDto> GetProductDetailAsync(int id);

    }
}
