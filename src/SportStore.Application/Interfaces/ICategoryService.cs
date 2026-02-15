using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request);
        Task UpdateCategoryAsync(int id, UpdateCategoryRequest request);
        Task DeleteCategoryAsync(int id);
    }
}
