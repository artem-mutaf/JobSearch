using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;

namespace JobBoard.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        category.Id = Guid.NewGuid();
        await _categoryRepository.AddAsync(category);
        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        if (await _categoryRepository.GetByIdAsync(category.Id) == null)
            throw new Exception("Category not found.");

        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        if (await _categoryRepository.GetByIdAsync(id) == null)
            throw new Exception("Category not found.");

        await _categoryRepository.DeleteAsync(id);
    }
}