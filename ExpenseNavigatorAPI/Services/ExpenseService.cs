using ExpenseNavigatorAPI.DAL;
using ExpenseNavigatorAPI.Data;
using ExpenseNavigatorAPI.Models;

namespace ExpenseNavigatorAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly List<Expense> _expenses = new List<Expense>();
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Expense> GetAll()
        {
            return _context.Expenses.ToList();
        }

        public Expense GetById(Guid id)
        {
            return _context.Expenses.FirstOrDefault(e => e.Id == id);
        }

        public Expense Add(Expense dto)
        {
            // Validate user
            if (!_context.Users.Any(u => u.Id == dto.UserId))
                throw new Exception($"User with Id {dto.UserId} does not exist.");

            if (!_context.Categories.Any(c => c.Id == dto.CategoryId))
                throw new Exception($"Category with Id {dto.CategoryId} does not exist.");

            if (dto.SubCategoryId.HasValue && !_context.SubCategories.Any(sc => sc.Id == dto.SubCategoryId.Value))
                throw new Exception($"SubCategory with Id {dto.SubCategoryId.Value} does not exist.");

            if (dto.PlaceId.HasValue && !_context.Places.Any(p => p.Id == dto.PlaceId.Value))
                throw new Exception($"Place with Id {dto.PlaceId.Value} does not exist.");

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                SubCategoryId = dto.SubCategoryId,
                PlaceId = dto.PlaceId,
                Amount = dto.Amount,
                PaidFor = dto.PaidFor,
                Note = dto.Note,
                Date = DateTime.UtcNow,
                IsFixed = dto.IsFixed,
            };

            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return expense;
        }

        public bool Update(ExpenseDto dto, Guid id)
        {
            var existing = _context.Expenses.FirstOrDefault(e => e.Id == id);
            if (existing == null) return false;

            existing.UserId = dto.UserId;
            existing.CategoryId = dto.CategoryId;
            existing.SubCategoryId = dto.SubCategoryId;
            existing.PlaceId = dto.PlaceId;
            existing.Amount = dto.Amount;
            existing.PaidFor = dto.PaidFor;
            existing.Note = dto.Note;
            existing.IsFixed = dto.IsFixed;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null) return false;

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Expense> GetByMonthYear(int month, int year)
        {
            return _context.Expenses
                .Where(e => e.Date.Month == month && e.Date.Year == year)
                .ToList();
        }
    }
}
