using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing employers using Entity Framework and an injected DbContext.
    /// </summary>
    public class EmployersService
    {
        private readonly ApplicationDbContext _context;

        public EmployersService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all employers asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of employers.</returns>
        public async Task<IEnumerable<EmployersModel>> GetEmployersAsync()
        {
            return await _context.Employers.ToListAsync();
        }

        /// <summary>
        /// Get an employer by their unique ID asynchronously.
        /// </summary>
        /// <param name="employerID">The ID of the employer to retrieve.</param>
        /// <returns>An asynchronous operation that returns the employer with the specified ID.</returns>
        public async Task<EmployersModel?> GetEmployerAsync(int employerID)
        {
            return await _context.Employers.FirstOrDefaultAsync(e => e.EmployerID == employerID);
        }

        /// <summary>
        /// Create a new employer asynchronously.
        /// </summary>
        /// <param name="employer">The employer object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created employer.</returns>
        public async Task<EmployersModel> CreateEmployerAsync(EmployersModel employer)
        {
            employer.EmployerID = 0; // EF Core will auto-generate the ID
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return employer;
        }

        /// <summary>
        /// Update an existing employer asynchronously.
        /// </summary>
        /// <param name="employerID">The ID of the employer to update.</param>
        /// <param name="employer">The updated employer object.</param>
        /// <returns>An asynchronous operation to update the employer.</returns>
        public async Task UpdateEmployerAsync(int employerID, EmployersModel employer)
        {
            var existingEmployer = await _context.Employers.FirstOrDefaultAsync(e => e.EmployerID == employerID);
            if (existingEmployer != null)
            {
                existingEmployer.CompanyName = employer.CompanyName;
                existingEmployer.CompanyDescription = employer.CompanyDescription;
                existingEmployer.CompanyLogo = employer.CompanyLogo;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete an employer by their unique ID asynchronously.
        /// </summary>
        /// <param name="employerID">The ID of the employer to delete.</param>
        /// <returns>An asynchronous operation to delete the employer.</returns>
        public async Task DeleteEmployerAsync(int employerID)
        {
            var employerToRemove = await _context.Employers.FirstOrDefaultAsync(e => e.EmployerID == employerID);
            if (employerToRemove != null)
            {
                _context.Employers.Remove(employerToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
