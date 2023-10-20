using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<CategoriesModel>> GetCategories()
        {
            return Ok(_categoriesService.GetCategories());
        }

        /// <summary>
        /// Get a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<CategoriesModel> GetCategory(int id)
        {
            var category = _categoriesService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        [HttpPost]
        public ActionResult<CategoriesModel> CreateCategory(CategoriesModel category)
        {
            var createdCategory = _categoriesService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryID }, createdCategory);
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoriesModel category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _categoriesService.UpdateCategory(id, category);

            return NoContent();
        }

        /// <summary>
        /// Delete a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _categoriesService.DeleteCategory(id);
            return NoContent();
        }
    }
}
