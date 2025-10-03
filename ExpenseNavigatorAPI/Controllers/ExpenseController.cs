using ExpenseNavigatorAPI.DAL;
using ExpenseNavigatorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseNavigatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expenses = _expenseService.GetAll();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var expense = _expenseService.GetById(id);
            if (expense == null) return NotFound();
            return Ok(expense);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ExpenseDto expenseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                UserId = expenseDto.UserId,
                CategoryId = expenseDto.CategoryId,
                SubCategoryId = expenseDto.SubCategoryId,
                PlaceId = expenseDto.PlaceId,
                Amount = expenseDto.Amount,
                PaidFor = expenseDto.PaidFor,
                Note = expenseDto.Note,
                IsFixed = expenseDto.IsFixed,
                Date = DateTime.UtcNow
            };

            var added = _expenseService.Add(expense);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _expenseService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("report")]
        public IActionResult GetReport([FromQuery] int month, [FromQuery] int year)
        {
            var report = _expenseService.GetByMonthYear(month, year);
            return Ok(report);
        }
    }

}
// I should add update