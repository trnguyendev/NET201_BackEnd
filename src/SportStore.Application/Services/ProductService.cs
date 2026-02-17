using AutoMapper; // Cần cài gói AutoMapper
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService; // Service xử lý file
        private readonly IMapper _mapper;           // Service map đối tượng

        // Constructor: Inject các service cần thiết
        public ProductService(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            // Dùng AutoMapper cho gọn code
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
        {
            // 1. Validate Logic nghiệp vụ
            if (request.Price < 0) throw new Exception("Giá sản phẩm không được nhỏ hơn 0!");

            // 2. Map từ DTO sang Entity (Chưa có ảnh)
            var product = new Product
            {
                Name = request.Name,
                BasePrice = request.Price,
                Description = request.Description,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId,
                IsActive = true,
                Thumbnail = "", // Sẽ cập nhật sau khi có ảnh Main
                ProductImages = new List<ProductImage>()
            };

            // 3. LƯU LẦN 1: Để Database sinh ID cho Product
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            // -> Lúc này product.Id đã có (ví dụ: 105)

            // Chuẩn bị đường dẫn folder: products/product-105
            string subFolder = Path.Combine("products", $"product-{product.Id}");
            string[] allowedExt = new[] { ".jpg", ".png", ".jpeg", ".webp" };
            bool hasImageUpdate = false;

            // 4. XỬ LÝ ẢNH ĐẠI DIỆN (MAIN IMAGE)
            if (request.MainImage != null)
            {
                // Lưu file vật lý
                string mainPath = await _fileService.SaveFileAsync(request.MainImage, subFolder, allowedExt);

                // Tạo Entity ProductImage
                var mainImgEntity = new ProductImage
                {
                    ProductId = product.Id,
                    ImageUrl = mainPath,
                    IsMain = true,   // Đánh dấu là ảnh bìa
                    ColorId = null   // Ảnh bìa thường không gắn màu cụ thể (hoặc tùy logic)
                };

                await _unitOfWork.ProductImages.AddAsync(mainImgEntity);

                // Cập nhật ngược lại vào bảng Product để query nhanh cho trang Home
                product.Thumbnail = mainPath;
                _unitOfWork.Products.Update(product);

                hasImageUpdate = true;
            }

            // 5. XỬ LÝ ẢNH BIẾN THỂ (VARIANT IMAGES)
            if (request.VariantImages != null && request.VariantImages.Count > 0)
            {
                foreach (var item in request.VariantImages)
                {
                    if (item.ImageFile != null)
                    {
                        string path = await _fileService.SaveFileAsync(item.ImageFile, subFolder, allowedExt);

                        var variantImgEntity = new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = path,
                            IsMain = false,       // Không phải ảnh bìa
                            ColorId = item.ColorId // Gắn với màu user chọn (ví dụ: Màu Đỏ)
                        };

                        await _unitOfWork.ProductImages.AddAsync(variantImgEntity);
                        hasImageUpdate = true;
                    }
                }
            }

            // 6. LƯU LẦN 2: Nếu có thay đổi về ảnh
            if (hasImageUpdate)
            {
                await _unitOfWork.CompleteAsync();
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(int id, CreateProductRequest request)
        {
            // Logic Update cũng cần xử lý tương tự:
            // - Nếu user up MainImage mới -> Xóa MainImage cũ (file + db) -> Lưu cái mới.
            // - Nếu user up Variant mới -> Thêm vào list.
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                // 1. Xóa dữ liệu trong DB
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.CompleteAsync();

                // 2. Dọn dẹp rác: Xóa folder ảnh vật lý để tiết kiệm ổ cứng
                // Đường dẫn: images/products/product-{id}
                string folderPath = Path.Combine("products", $"product-{id}");
                _fileService.DeleteFile(folderPath); // Cần update hàm DeleteFile để xóa cả folder
            }
        }

        public async Task<IEnumerable<ProductHomeDto>> GetHomeProductsAsync()
        {
            return await _unitOfWork.Products.GetHomeProductsAsync();
        }
    }
}