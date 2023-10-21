using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing employer's job listings using Entity Framework and an injected DbContext.
    /// </summary>
    public class EmployersJobListingsService
    {
        private readonly ApplicationDbContext _context;

        public EmployersJobListingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all employer's job listings asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of employer's job listings.</returns>
        public async Task<IEnumerable<EmployersJobListingsModel>> GetEmployersJobListingsAsync()
        {
            return await _context.EmployersJobListings.ToListAsync();
        }

        /// <summary>
        /// Get an employer's job listing by its unique ID asynchronously.
        /// </summary>
        /// <param name="employersJobListingID">The ID of the employer's job listing to retrieve.</param>
        /// <returns>An asynchronous operation that returns the employer's job listing with the specified ID.</returns>
        public async Task<EmployersJobListingsModel?> GetEmployersJobListingAsync(int employersJobListingID)
        {
            return await _context.EmployersJobListings.FirstOrDefaultAsync(ejl => ejl.EmployersJobListingID == employersJobListingID);
        }

        /// <summary>
        /// Create a new employer's job listing asynchronously.
        /// </summary>
        /// <param name="employersJobListing">The employer's job listing object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created employer's job listing.</returns>
        public async Task<EmployersJobListingsModel> CreateEmployersJobListingAsync(EmployersJobListingsModel employersJobListing)
        {
            employersJobListing.EmployersJobListingID = 0; // EF Core will auto-generate the ID
            _context.EmployersJobListings.Add(employersJobListing);
            await _context.SaveChangesAsync();
            return employersJobListing;
        }

        /// <summary>
        /// Delete an employer's job listing by its unique ID asynchronously.
        /// </summary>
        /// <param name="employersJobListingID">The ID of the employer's job listing to delete.</param>
        /// <returns>An asynchronous operation to delete the employer's job listing.</returns>
        public async Task DeleteEmployersJobListingAsync(int employersJobListingID)
        {
            var employerJobListingToRemove = await _context.EmployersJobListings.FirstOrDefaultAsync(ejl => ejl.EmployersJobListingID == employersJobListingID);
            if (employerJobListingToRemove != null)
            {
                _context.EmployersJobListings.Remove(employerJobListingToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
