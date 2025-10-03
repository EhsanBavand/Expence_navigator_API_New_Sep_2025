using ExpenseNavigatorAPI.DAL;
using ExpenseNavigatorAPI.Data;
using ExpenseNavigatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseNavigatorAPI.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SubCategory> GetAllSubCategories()
        {
            var subCategories = _context.SubCategories
                                        .ToList();
            return subCategories;
        }

        public SubCategory GetSubCategoryById(Guid id)
        {
            var subCategory = _context.SubCategories
                                      .FirstOrDefault(sc => sc.Id == id);
            return subCategory;
        }

        public SubCategory AddSubCategory(SubCategory subCategory)
        {
            subCategory.Id = Guid.NewGuid();
            subCategory.CreatedDate = DateTime.UtcNow;

            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();

            return subCategory;
        }

        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            var existing = _context.SubCategories.Find(subCategory.Id);
            if (existing == null) return null;

            existing.Name = subCategory.Name;
            existing.CategoryId = subCategory.CategoryId;

            _context.SaveChanges();
            return existing;
        }

        public bool DeleteSubCategory(Guid id)
        {
            var subCategory = _context.SubCategories.Find(id);
            if (subCategory == null) return false;

            _context.SubCategories.Remove(subCategory);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<SubCategory> GetSubCategoriesByCategoryId(Guid categoryId)
        {
            return _context.SubCategories
                           .Where(sc => sc.CategoryId == categoryId)
                           .ToList();
        }
    }



}
