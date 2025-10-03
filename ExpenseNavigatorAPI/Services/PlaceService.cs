using ExpenseNavigatorAPI.Data;
using ExpenseNavigatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseNavigatorAPI.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationDbContext _context;

        public PlaceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Place> GetAllPlaces()
        {
            return _context.Places.Include(p => p.Category)
                                  .Include(p => p.SubCategory)
                                  .ToList();
        }

        public Place GetPlaceById(Guid id)
        {
            return _context.Places.Include(p => p.Category)
                                  .Include(p => p.SubCategory)
                                  .FirstOrDefault(p => p.Id == id);
        }

        public Place AddPlace(PlaceDto dto)
        {
            if (!_context.Users.Any(u => u.Id == dto.UserId))
                throw new Exception($"User with Id {dto.UserId} does not exist.");

            if (!_context.Categories.Any(c => c.Id == dto.CategoryId))
                throw new Exception($"Category with Id {dto.CategoryId} does not exist.");

            if (dto.SubCategoryId.HasValue && !_context.SubCategories.Any(sc => sc.Id == dto.SubCategoryId.Value))
                throw new Exception($"SubCategory with Id {dto.SubCategoryId.Value} does not exist.");

            var place = new Place
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                SubCategoryId = dto.SubCategoryId
            };

            _context.Places.Add(place);
            _context.SaveChanges();

            return place;
        }

        public Place UpdatePlace(PlaceDto dto)
        {
            var existing = _context.Places.Find(dto.Id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.UserId = dto.UserId;
            existing.CategoryId = dto.CategoryId;
            existing.SubCategoryId = dto.SubCategoryId;

            _context.SaveChanges();
            return existing;
        }

        public bool DeletePlace(Guid id)
        {
            var place = _context.Places.Find(id);
            if (place == null) return false;

            _context.Places.Remove(place);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<PlaceDropdownDto> GetPlacesForDropdown(Guid? categoryId = null, Guid? subCategoryId = null)
        {
            var query = _context.Places.AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            if (subCategoryId.HasValue)
                query = query.Where(p => p.SubCategoryId == subCategoryId);

            return query.Select(p => new PlaceDropdownDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
    }
}
