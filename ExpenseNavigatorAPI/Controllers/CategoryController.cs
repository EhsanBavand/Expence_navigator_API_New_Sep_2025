using ExpenseNavigatorAPI.Models;
using ExpenseNavigatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenseNavigatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // GET /api/category?userId=...
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required.");

            var categories = await _service.GetAllCategories(userId);
            return Ok(categories);
        }

        // GET /api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _service.GetCategoryById(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        // POST /api/category
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.AddCategory(category);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT /api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != category.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateCategory(category);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // DELETE /api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteCategory(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
