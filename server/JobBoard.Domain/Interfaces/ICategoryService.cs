using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
}