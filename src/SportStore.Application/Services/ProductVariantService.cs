using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        public Task<IEnumerable<ProductVariantDto>> GetAllProductVariantsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariantDto> GellProductVariantByIdAsnyc(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariantDto> CreateProductVarianAsync(CreateProductVariantRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductVarianAsync(int id, UpdateProductVariantRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductVariantAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
