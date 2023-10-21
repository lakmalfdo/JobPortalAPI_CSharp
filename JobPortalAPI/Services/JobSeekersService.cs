using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job seekers using Entity Framework and an injected DbContext.
    /// </summary>
    public class JobSeekersService
    {
        private readonly ApplicationDbContext _context;

        public JobSeekersService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job seekers asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job seekers.</returns>
        public async Task<IEnumerable<JobSeekersModel>> GetJobSeekersAsync()
        {
            return await _context.JobSeekers.ToListAsync();
        }

        /// <summary>
        /// Get a job seeker by their unique ID asynchronously.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job seeker with the specified ID.</returns>
        public async Task<JobSeekersModel?> GetJobSeekerAsync(int jobSeekerID)
        {
            return await _context.JobSeekers.FirstOrDefaultAsync(js => js.JobSeekerID == jobSeekerID);
        }

        /// <summary>
        /// Create a new job seeker asynchronously.
        /// </summary>
        /// <param name="jobSeeker">The job seeker object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job seeker.</returns>
        public async Task<JobSeekersModel> CreateJobSeekerAsync(JobSeekersModel jobSeeker)
        {
            jobSeeker.JobSeekerID = 0; // EF Core will auto-generate the ID
            _context.JobSeekers.Add(jobSeeker);
            await _context.SaveChangesAsync();
            return jobSeeker;
        }

        /// <summary>
        /// Update an existing job seeker asynchronously.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to update.</param>
        /// <param name="jobSeeker">The updated job seeker object.</param>
        /// <returns>An asynchronous operation to update the job seeker.</returns>
        public async Task UpdateJobSeekerAsync(int jobSeekerID, JobSeekersModel jobSeeker)
        {
            var existingJobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.JobSeekerID == jobSeekerID);
            if (existingJobSeeker != null)
            {
                existingJobSeeker.Resume = jobSeeker.Resume;
                existingJobSeeker.CoverLetter = jobSeeker.CoverLetter;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a job seeker by their unique ID asynchronously.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to delete.</param>
        /// <returns>An asynchronous operation to delete the job seeker.</returns>
        public async Task DeleteJobSeekerAsync(int jobSeekerID)
        {
            var jobSeekerToRemove = await _context.JobSeekers.FirstOrDefaultAsync(js => js.JobSeekerID == jobSeekerID);
            if (jobSeekerToRemove != null)
            {
                _context.JobSeekers.Remove(jobSeekerToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
