using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<BrandDto> GetBrandByIdAsync (int id);
        Task<BrandDto> CreateBrandAsync (CreateBrandRequest request);
        Task UpdateBrandAsync (int id, UpdateBrandRequest request);
        Task DeleteBrandAsync (int id);
    }
}
