using ExpenseNavigatorAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseNavigatorAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories(string userId);
        Task<Category> GetCategoryById(Guid id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Guid id);
    }
}
