using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job category mappings using Entity Framework and an injected DbContext.
    /// </summary>
    public class JobCategoryMappingService
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryMappingService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job category mappings asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job category mappings.</returns>
        public async Task<IEnumerable<JobCategoryMappingModel>> GetJobCategoryMappingsAsync()
        {
            return await _context.JobCategoryMappings.ToListAsync();
        }

        /// <summary>
        /// Get a job category mapping by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobCategoryMappingID">The ID of the job category mapping to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job category mapping with the specified ID.</returns>
        public async Task<JobCategoryMappingModel?> GetJobCategoryMappingAsync(int jobCategoryMappingID)
        {
            return await _context.JobCategoryMappings.FirstOrDefaultAsync(jcm => jcm.JobCategoryMappingID == jobCategoryMappingID);
        }

        /// <summary>
        /// Create a new job category mapping asynchronously.
        /// </summary>
        /// <param name="jobCategoryMapping">The job category mapping object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job category mapping.</returns>
        public async Task<JobCategoryMappingModel> CreateJobCategoryMappingAsync(JobCategoryMappingModel jobCategoryMapping)
        {
            jobCategoryMapping.JobCategoryMappingID = 0; // EF Core will auto-generate the ID
            _context.JobCategoryMappings.Add(jobCategoryMapping);
            await _context.SaveChangesAsync();
            return jobCategoryMapping;
        }

        /// <summary>
        /// Delete a job category mapping by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobCategoryMappingID">The ID of the job category mapping to delete.</param>
        /// <returns>An asynchronous operation to delete the job category mapping.</returns>
        public async Task DeleteJobCategoryMappingAsync(int jobCategoryMappingID)
        {
            var jobCategoryMappingToRemove = await _context.JobCategoryMappings.FirstOrDefaultAsync(jcm => jcm.JobCategoryMappingID == jobCategoryMappingID);
            if (jobCategoryMappingToRemove != null)
            {
                _context.JobCategoryMappings.Remove(jobCategoryMappingToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
