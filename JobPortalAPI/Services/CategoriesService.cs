using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job categories using Entity Framework and an injected DbContext.
    /// </summary>
    public class CategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job categories asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job categories.</returns>
        public async Task<IEnumerable<CategoriesModel>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Get a job category by its unique ID asynchronously.
        /// </summary>
        /// <param name="categoryID">The ID of the category to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job category with the specified ID.</returns>
        public async Task<CategoriesModel?> GetCategoryAsync(int categoryID)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryID);
        }

        /// <summary>
        /// Create a new job category asynchronously.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job category.</returns>
        public async Task<CategoriesModel> CreateCategoryAsync(CategoriesModel category)
        {
            category.CategoryID = 0; // EF Core will auto-generate the ID
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Update an existing job category asynchronously.
        /// </summary>
        /// <param name="categoryID">The ID of the category to update.</param>
        /// <param name="category">The updated category object.</param>
        /// <returns>An asynchronous operation to update the job category.</returns>
        public async Task UpdateCategoryAsync(int categoryID, CategoriesModel category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.CategoryDescription = category.CategoryDescription;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a job category by its unique ID asynchronously.
        /// </summary>
        /// <param name="categoryID">The ID of the category to delete.</param>
        /// <returns>An asynchronous operation to delete the job category.</returns>
        public async Task DeleteCategoryAsync(int categoryID)
        {
            var categoryToRemove = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryID);
            if (categoryToRemove != null)
            {
                _context.Categories.Remove(categoryToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
