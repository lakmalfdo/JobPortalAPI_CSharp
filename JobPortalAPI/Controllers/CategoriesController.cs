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
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoriesService categoriesService, ILogger<CategoriesController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesModel>>> GetCategories()
        {
            try
            {
                var categories = await _categoriesService.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoriesController)}.{nameof(GetCategories)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching categories.");
            }
        }

        /// <summary>
        /// Get a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesModel>> GetCategory(int id)
        {
            try
            {
                var category = await _categoriesService.GetCategoryAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoriesController)}.{nameof(GetCategory)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the category.");
            }
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        [HttpPost]
        public async Task<ActionResult<CategoriesModel>> CreateCategory(CategoriesModel category)
        {
            try
            {
                var createdCategory = await _categoriesService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryID }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoriesController)}.{nameof(CreateCategory)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoriesModel category)
        {
            try
            {
                if (id != category.CategoryID)
                {
                    return BadRequest();
                }

                await _categoriesService.UpdateCategoryAsync(id, category);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoriesController)}.{nameof(UpdateCategory)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        /// <summary>
        /// Delete a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoriesService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoriesController)}.{nameof(DeleteCategory)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
