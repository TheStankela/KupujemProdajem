using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using KupujemProdajem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KupujemProdajemDbContext _context;
        public CategoryRepository(KupujemProdajemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        }
        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _context.Categories.AnyAsync(x => x.Id == categoryId);
        }

        public async Task<bool> CreateCategory(CategoryModel category)
        {
            await _context.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(CategoryModel category)
        {
             _context.Remove(category);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
