using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            //Map thủ công
            return categories.Select(c => new CategoryDto
            {
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
            });
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryDto { Name = category.Name, DisplayOrder = category.DisplayOrder };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                DisplayOrder = request.DisplayOrder,
            };
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return new CategoryDto { Name = request.Name, DisplayOrder = request.DisplayOrder };
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if(category != null)
            {
                category.Name = request.Name;
                category.DisplayOrder = request.DisplayOrder;
                
                _unitOfWork.Categories.Update(category);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.Categories.Delete(category);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
