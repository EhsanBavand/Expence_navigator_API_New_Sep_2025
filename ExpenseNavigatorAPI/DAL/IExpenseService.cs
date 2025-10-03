using ExpenseNavigatorAPI.Models;

namespace ExpenseNavigatorAPI.DAL
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAll();
        Expense GetById(Guid id);
        Expense Add(Expense dto);
        bool Update(ExpenseDto dto, Guid id);
        bool Delete(Guid id);

        // Report by month/year
        IEnumerable<Expense> GetByMonthYear(int month, int year);
    }

}
