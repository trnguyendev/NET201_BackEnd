using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductColorService : IProductColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductColorDto>> GetAllProductColorsAsync()
        {
            var productSizes = await _unitOfWork.ProductColors.GetAllAsync();
            return productSizes.Select(item => new ProductColorDto
            {
                Id = item.Id,
                Name = item.Name,
                HexCode = item.HexCode,
            });
        }

        public async Task<ProductColorDto> GetProductColorByIdAsync(int id)
        {
            var productSize = await _unitOfWork.ProductColors.GetByIdAsync(id);
            if (productSize == null) return null;
            return new ProductColorDto { Name = productSize.Name, HexCode = productSize.HexCode };
        }

        public async Task<ProductColorDto> CreateProductColorAsync(CreateProductColorRequest request)
        {
            var productColor = new ProductColor
            {
                Name = request.Name,
                HexCode = request.HexCode,
            };
            await _unitOfWork.ProductColors.AddAsync(productColor);
            await _unitOfWork.CompleteAsync();
            return new ProductColorDto { Name = request.Name, HexCode = request.HexCode };
        }

        public async Task UpdateProductColorAsync(int id, UpdateProductColorRequest reques)
        {
            var productColor = await _unitOfWork.ProductColors.GetByIdAsync(id);
            if (productColor != null)
            {
                productColor.Name = reques.Name;
                productColor.HexCode = reques.HexCode;

                _unitOfWork.ProductColors.Update(productColor);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteProductColorAsync(int id)
        {
            var productColor = await _unitOfWork.ProductColors.GetByIdAsync(id);
            if (productColor != null)
            {
                _unitOfWork.ProductColors.Delete(productColor);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
