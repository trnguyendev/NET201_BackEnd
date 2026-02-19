using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            // Tạm thời map thủ công, nếu bạn xài AutoMapper thì càng ngắn gọn
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive,
                BasePrice = p.BasePrice,
                Thumbnail = p.Thumbnail,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var p = await _unitOfWork.Products.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive,
                BasePrice = p.BasePrice,
                Thumbnail = p.Thumbnail,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId
            };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
        {
            string? thumbnailUrl = null;

            // Xử lý upload Thumbnail (lưu vào thư mục "products")
            if (request.ThumbnailFile != null)
            {
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                thumbnailUrl = await _fileService.SaveFileAsync(request.ThumbnailFile, "products", allowedExtensions);
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                BasePrice = request.BasePrice,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId,
                Thumbnail = thumbnailUrl
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return new ProductDto { Id = product.Id, Name = product.Name, Thumbnail = product.Thumbnail };
        }

        public async Task UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.IsActive = request.IsActive;
                product.BasePrice = request.BasePrice;
                product.CategoryId = request.CategoryId;
                product.BrandId = request.BrandId;

                // Nếu có chọn Thumbnail mới
                if (request.ThumbnailFile != null)
                {
                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(product.Thumbnail))
                    {
                        _fileService.DeleteFile(product.Thumbnail);
                    }

                    // Upload ảnh mới
                    string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    product.Thumbnail = await _fileService.SaveFileAsync(request.ThumbnailFile, "products", allowedExtensions);
                }

                _unitOfWork.Products.Update(product);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                // Xóa file ảnh Thumbnail trong thư mục
                if (!string.IsNullOrEmpty(product.Thumbnail))
                {
                    _fileService.DeleteFile(product.Thumbnail);
                }

                _unitOfWork.Products.Delete(product);
                await _unitOfWork.CompleteAsync();
            }
        }

        public Task<IEnumerable<ProductHomeDto>> GetHomeProductsAsync()
        {
            // Tạm thời để trống hàm này (hoặc bạn có thể implement theo logic riêng sau)
            throw new NotImplementedException();
        }
    }
}