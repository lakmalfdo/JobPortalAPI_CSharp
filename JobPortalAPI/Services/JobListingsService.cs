using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job listings using Entity Framework and an injected DbContext.
    /// </summary>
    public class JobListingsService
    {
        private readonly ApplicationDbContext _context;

        public JobListingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job listings asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job listings.</returns>
        public async Task<IEnumerable<JobListingsModel>> GetJobListingsAsync()
        {
            return await _context.JobListings.ToListAsync();
        }

        /// <summary>
        /// Get a job listing by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job listing with the specified ID.</returns>
        public async Task<JobListingsModel?> GetJobListingAsync(int jobID)
        {
            return await _context.JobListings.FirstOrDefaultAsync(j => j.JobID == jobID);
        }

        /// <summary>
        /// Create a new job listing asynchronously.
        /// </summary>
        /// <param name="jobListing">The job listing object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job listing.</returns>
        public async Task<JobListingsModel> CreateJobListingAsync(JobListingsModel jobListing)
        {
            jobListing.JobID = 0; // EF Core will auto-generate the ID
            _context.JobListings.Add(jobListing);
            await _context.SaveChangesAsync();
            return jobListing;
        }

        /// <summary>
        /// Update an existing job listing asynchronously.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to update.</param>
        /// <param name="jobListing">The updated job listing object.</param>
        /// <returns>An asynchronous operation to update the job listing.</returns>
        public async Task UpdateJobListingAsync(int jobID, JobListingsModel jobListing)
        {
            var existingJobListing = await _context.JobListings.FirstOrDefaultAsync(j => j.JobID == jobID);
            if (existingJobListing != null)
            {
                existingJobListing.EmployerID = jobListing.EmployerID;
                existingJobListing.JobTitle = jobListing.JobTitle;
                existingJobListing.JobDescription = jobListing.JobDescription;
                existingJobListing.JobRequirements = jobListing.JobRequirements;
                existingJobListing.Salary = jobListing.Salary;
                existingJobListing.Location = jobListing.Location;
                existingJobListing.ApplicationDeadline = jobListing.ApplicationDeadline;
                existingJobListing.ApplicationStatus = jobListing.ApplicationStatus;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a job listing by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to delete.</param>
        /// <returns>An asynchronous operation to delete the job listing.</returns>
        public async Task DeleteJobListingAsync(int jobID)
        {
            var jobListingToRemove = await _context.JobListings.FirstOrDefaultAsync(j => j.JobID == jobID);
            if (jobListingToRemove != null)
            {
                _context.JobListings.Remove(jobListingToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
