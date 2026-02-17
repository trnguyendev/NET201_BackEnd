using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class BrandService : IBrandService
    {
        IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return new BrandDto { Name = brand.Name, LogoUrl = brand.LogoUrl };
        }

        public async Task<BrandDto> CreateBrandAsync(CreateBrandRequest request)
        {
            var brand = new Brand
            {
                Name = request.Name,
                LogoUrl = request.LogoUrl,
            };
            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.CompleteAsync();
            return new BrandDto { Name = request.Name, LogoUrl = request.LogoUrl };
        }

        public async Task UpdateBrandAsync(int id, UpdateBrandRequest request)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand != null)
            {
                brand.Name = request.Name;
                brand.LogoUrl = request.LogoUrl;

                _unitOfWork.Brands.Update(brand);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if(brand != null)
            {
                _unitOfWork.Brands.Delete(brand);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
