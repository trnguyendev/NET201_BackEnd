using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            // Map thủ công (Manual Map) để bạn dễ hiểu. Thực tế nên dùng AutoMapper.
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.BasePrice
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto { Id = product.Id, Name = product.Name, Price = product.BasePrice };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
        {
            // Logic nghiệp vụ: Ví dụ giá không được âm
            if (request.Price < 0) throw new Exception("Giá sản phẩm không được nhỏ hơn 0!");

            var product = new Product
            {
                Name = request.Name,
                BasePrice = request.Price,
                Description = request.Description,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync(); // Lưu xuống DB

            return new ProductDto { Id = product.Id, Name = product.Name, Price = product.BasePrice };
        }

        public async Task UpdateProductAsync(int id, CreateProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                product.Name = request.Name;
                product.BasePrice = request.Price;
                product.Description = request.Description;

                _unitOfWork.Products.Update(product);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
