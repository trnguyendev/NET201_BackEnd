using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public BrandService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Brands.GetAllAsync();
            return brands.Select(item => new BrandDto
            {
                Id = item.Id,
                Name = item.Name,
                LogoUrl = item.LogoUrl
            });
        }

        public async Task<BrandDto> GetBrandByIdAsync(int id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand == null) return null;
            return new BrandDto { Id = brand.Id, Name = brand.Name, LogoUrl = brand.LogoUrl };
        }

        public async Task<BrandDto> CreateBrandAsync(CreateBrandRequest request)
        {
            string? logoUrl = null;

            // Nếu có file upload lên, gọi hàm SaveFileAsync
            if (request.LogoFile != null)
            {
                // Truyền "brands" làm subFolder và giới hạn các đuôi file ảnh
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                logoUrl = await _fileService.SaveFileAsync(request.LogoFile, "brands", allowedExtensions);
            }

            var brand = new Brand
            {
                Name = request.Name,
                LogoUrl = logoUrl // Gán URL vừa upload được (hoặc null)
            };

            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.CompleteAsync();

            return new BrandDto { Id = brand.Id, Name = brand.Name, LogoUrl = brand.LogoUrl };
        }

        public async Task UpdateBrandAsync(int id, UpdateBrandRequest request)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand != null)
            {
                brand.Name = request.Name;

                // Nếu người dùng chọn upload ảnh mới
                if (request.LogoFile != null)
                {
                    // 1. Xóa ảnh cũ (nếu có) để không bị rác server
                    if (!string.IsNullOrEmpty(brand.LogoUrl))
                    {
                        _fileService.DeleteFile(brand.LogoUrl);
                    }

                    // 2. Upload ảnh mới
                    string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    brand.LogoUrl = await _fileService.SaveFileAsync(request.LogoFile, "brands", allowedExtensions);
                }

                _unitOfWork.Brands.Update(brand);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand != null)
            {
                // Khi xóa Brand, xóa luôn file ảnh logo đi kèm (nếu có)
                if (!string.IsNullOrEmpty(brand.LogoUrl))
                {
                    _fileService.DeleteFile(brand.LogoUrl);
                }

                _unitOfWork.Brands.Delete(brand);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
