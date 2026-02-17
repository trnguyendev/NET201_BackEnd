using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductSizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductSizeDto>> GetAllProductSizesAsync()
        {
            var productSizes = await _unitOfWork.ProductSizes.GetAllAsync();
            return productSizes.Select(item => new ProductSizeDto
            {
                Name = item.Name,
                Type = item.Type,
            });
        }

        public async Task<ProductSizeDto> GetProductByIdAsync(int id)
        {
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if (productSize == null) return null;
            return new ProductSizeDto { Name = productSize.Name, Type = productSize.Type };
        }

        public async Task<ProductSizeDto> CreateProductSizeAsync(CreateProductSizeRequest request)
        {
            var productSize = new ProductSize
            {
                Name = request.Name,
                Type = request.Type,
            };
            await _unitOfWork.ProductSizes.AddAsync(productSize);
            await _unitOfWork.CompleteAsync();
            return new ProductSizeDto { Name = request.Name, Type = request.Type };
        }

        public async Task UpdateProductSizeAsync(int id, UpdateProductSizeRequest request)
        {
            var productSzie = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if (productSzie != null)
            {
                productSzie.Name = request.Name;
                productSzie.Type = request.Type;

                _unitOfWork.ProductSizes.Update(productSzie);
                await _unitOfWork.CompleteAsync();
            }
            ;
        }

        public async Task DeleteProductSizeAsync(int id)
        {
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if(productSize != null)
            {
                _unitOfWork.ProductSizes.Delete(productSize);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
