using ExpenseNavigatorAPI.Data;
using ExpenseNavigatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseNavigatorAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all categories for a user
        public async Task<IEnumerable<Category>> GetAllCategories(string userId)
        {
            return await _context.Categories
                                 .Include(c => c.SubCategories)
                                 .Include(c => c.Places)
                                 .Where(c => c.UserId == userId)
                                 .ToListAsync();
        }

        // Get category by its ID
        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _context.Categories
                                 .Include(c => c.SubCategories)
                                 .Include(c => c.Places)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Add a new category
        public async Task<Category> AddCategory(Category category)
        {
            category.Id = Guid.NewGuid();
            category.CreatedDate = DateTime.UtcNow;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // Update existing category
        public async Task<Category> UpdateCategory(Category category)
        {
            var existing = await _context.Categories
                                         .Include(c => c.SubCategories)
                                         .Include(c => c.Places)
                                         .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existing == null) return null;

            existing.Name = category.Name;
            existing.IsActive = category.IsActive;
            existing.UserId = category.UserId; // optional if user can change it

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete category by ID
        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(s => s.Expenses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return false;

            // Delete related expenses
            foreach (var sub in category.SubCategories)
                _context.Expenses.RemoveRange(sub.Expenses);

            // Delete subcategories
            _context.SubCategories.RemoveRange(category.SubCategories);

            // Delete category
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
