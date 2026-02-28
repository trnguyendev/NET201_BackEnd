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

        public async Task<PageResult<ProductHomeDto>> GetHomeProductsAsync(int pageNumber = 1, int pageSize = 20)
        {
            // 1. Tạo câu truy vấn bằng GetQueryable() (Lúc này CHƯA chạy xuống DB)
            var query = _unitOfWork.Products.GetQueryable().Where(p => p.IsActive == true);

            // 2. Đếm tổng số lượng (Database chỉ chạy lệnh COUNT(*) cực kỳ nhẹ)
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // 3. Thực hiện phân trang (Lệnh Skip & Take sẽ được dịch thành OFFSET FETCH trong SQL)
            // Gọi .ToList() thì Database mới bắt đầu nhả ra ĐÚNG 20 dòng sản phẩm!
            var pagedProducts = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 4. Lấy danh mục và thương hiệu (Vì bảng này thường ít dữ liệu nên GetAll tạm thời vẫn chấp nhận được)
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var brands = await _unitOfWork.Brands.GetAllAsync();

            // 5. Map dữ liệu
            var items = pagedProducts.Select(p => new ProductHomeDto
            {
                Id = p.Id,
                Name = p.Name,
                BasePrice = p.BasePrice,
                Thumbnail = p.Thumbnail,
                CategoryName = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name,
                BrandName = brands.FirstOrDefault(b => b.Id == p.BrandId)?.Name,
                BrandImg = brands.FirstOrDefault(b => b.Id == p.BrandId)?.LogoUrl,
            });

            // 6. Trả về gói dữ liệu phân trang
            return new PageResult<ProductHomeDto>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PageResult<ProductHomeDto>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 20)
        {
            // 1. Tạo câu truy vấn (Lúc này CHƯA chạy xuống DB)
            // Giả sử IRepository của bạn có hàm GetQueryable() trả về IQueryable<Product>
            var query = _unitOfWork.Products.GetQueryable()
                            .Where(p => p.CategoryId == categoryId && p.IsActive == true);

            // 2. Đếm tổng số lượng (Chỉ chạy câu lệnh COUNT(*) dưới DB, cực kỳ nhẹ)
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // 3. Phân trang và JOIN dữ liệu trực tiếp dưới Database
            // Entity Framework sẽ tự động dịch đoạn này thành câu lệnh SQL SELECT + JOIN + OFFSET FETCH
            var pagedProducts = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductHomeDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    BasePrice = p.BasePrice,
                    Thumbnail = p.Thumbnail,
                    // Nếu bạn đã setup Navigation Properties (Khóa ngoại) trong Entity, bạn lấy trực tiếp thế này:
                    CategoryName = p.Category.Name,
                    BrandName = p.Brand.Name,
                    BrandImg = p.Brand.LogoUrl
                })
                .ToList(); // Gọi ToList() lúc này DB mới thực thi và CHỈ trả về đúng 20 dòng!

            // 4. Trả về kết quả
            return new PageResult<ProductHomeDto>
            {
                Items = pagedProducts,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ProductDetailDto> GetProductDetailAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || !product.IsActive) return null; // Không bán hoặc ẩn thì không cho xem

            var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);
            var brand = await _unitOfWork.Brands.GetByIdAsync(product.BrandId);

            // Lấy danh sách biến thể của sản phẩm này
            var allVariants = await _unitOfWork.ProductVariants.GetAllAsync();
            var variants = allVariants.Where(v => v.ProductId == id).ToList();

            var colors = await _unitOfWork.ProductColors.GetAllAsync();
            var sizes = await _unitOfWork.ProductSizes.GetAllAsync();

            var variantDtos = variants.Select(v => new ProductVariantDto
            {
                Id = v.Id,
                ProductId = v.ProductId,
                ColorId = v.ColorId,
                ColorName = colors.FirstOrDefault(c => c.Id == v.ColorId)?.Name,
                SizeId = v.SizeId,
                SizeName = sizes.FirstOrDefault(s => s.Id == v.SizeId)?.Name,
                Quantity = v.Quantity,
                SkuCode = v.SkuCode
            }).ToList();

            return new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                Thumbnail = product.Thumbnail,
                CategoryName = category?.Name,
                BrandName = brand?.Name,
                Variants = variantDtos
            };
        }
    }
}