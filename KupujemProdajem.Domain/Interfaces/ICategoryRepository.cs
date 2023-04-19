using KupujemProdajem.Domain.Models;

namespace KupujemProdajem.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryExists(int categoryId);
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> GetCategoryByIdAsync(int categoryId);
        Task<bool> CreateCategory(CategoryModel category);
        Task<bool> DeleteCategory(CategoryModel category);
        Task<bool> Save();
    }
}