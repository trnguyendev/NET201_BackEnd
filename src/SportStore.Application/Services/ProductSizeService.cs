using Microsoft.EntityFrameworkCore; // 👉 Bắt buộc thêm dòng này để xài được lệnh .Include()
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
            // 👉 Dùng GetQueryable và Include để JOIN bảng Category
            var productSizes = await _unitOfWork.ProductSizes.GetQueryable()
                                        .Include(ps => ps.Category)
                                        .OrderBy(ps => ps.CategoryId) // Xếp theo danh mục
                                        .ThenBy(ps => ps.Name)        // Rồi xếp theo tên Size
                                        .ToListAsync();

            return productSizes.Select(item => new ProductSizeDto
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = item.CategoryId,
                CategoryName = item.Category?.Name // 👉 Lấy tên danh mục ra
            });
        }

        public async Task<ProductSizeDto> GetProductByIdAsync(int id)
        {
            // Tương tự, dùng GetQueryable để lấy cả tên Category
            var productSize = await _unitOfWork.ProductSizes.GetQueryable()
                                        .Include(ps => ps.Category)
                                        .FirstOrDefaultAsync(ps => ps.Id == id);

            if (productSize == null) return null;

            return new ProductSizeDto
            {
                Id = productSize.Id,
                Name = productSize.Name,
                CategoryId = productSize.CategoryId,
                CategoryName = productSize.Category?.Name
            };
        }

        public async Task<ProductSizeDto> CreateProductSizeAsync(CreateProductSizeRequest request)
        {
            var productSize = new ProductSize
            {
                Name = request.Name,
                CategoryId = request.CategoryId // 👉 Đổi Type thành CategoryId
            };

            await _unitOfWork.ProductSizes.AddAsync(productSize);
            await _unitOfWork.CompleteAsync();

            return new ProductSizeDto
            {
                Id = productSize.Id,
                Name = productSize.Name,
                CategoryId = productSize.CategoryId
            };
        }

        public async Task UpdateProductSizeAsync(int id, UpdateProductSizeRequest request)
        {
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if (productSize != null)
            {
                productSize.Name = request.Name;
                productSize.CategoryId = request.CategoryId;

                _unitOfWork.ProductSizes.Update(productSize);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteProductSizeAsync(int id)
        {
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if (productSize != null)
            {
                _unitOfWork.ProductSizes.Delete(productSize);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}