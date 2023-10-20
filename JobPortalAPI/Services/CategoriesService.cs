using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class CategoriesService
    {
        private readonly List<CategoriesModel> _categories = new List<CategoriesModel>();
        private int _nextCategoryID = 1;

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        public IEnumerable<CategoriesModel> GetCategories()
        {
            return _categories;
        }

        /// <summary>
        /// Get a category by its unique ID.
        /// </summary>
        /// <param name="categoryID">The ID of the category to retrieve.</param>
        public CategoriesModel GetCategory(int categoryID)
        {
            return _categories.FirstOrDefault(c => c.CategoryID == categoryID);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        public CategoriesModel CreateCategory(CategoriesModel category)
        {
            category.CategoryID = _nextCategoryID++;
            _categories.Add(category);
            return category;
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="categoryID">The ID of the category to update.</param>
        /// <param name="category">The updated category object.</param>
        public void UpdateCategory(int categoryID, CategoriesModel category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.CategoryDescription = category.CategoryDescription;
            }
        }

        /// <summary>
        /// Delete a category by its unique ID.
        /// </summary>
        /// <param name="categoryID">The ID of the category to delete.</param>
        public void DeleteCategory(int categoryID)
        {
            var categoryToRemove = _categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (categoryToRemove != null)
            {
                _categories.Remove(categoryToRemove);
            }
        }
    }
}
