using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job seeker skills using Entity Framework and an injected DbContext.
    /// </summary>
    public class JobSeekerSkillsService
    {
        private readonly ApplicationDbContext _context;

        public JobSeekerSkillsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job seeker skills asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job seeker skills.</returns>
        public async Task<IEnumerable<JobSeekerSkillsModel>> GetJobSeekerSkillsAsync()
        {
            return await _context.JobSeekerSkills.ToListAsync();
        }

        /// <summary>
        /// Get a job seeker skill by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job seeker skill with the specified ID.</returns>
        public async Task<JobSeekerSkillsModel?> GetJobSeekerSkillAsync(int jobSeekerSkillID)
        {
            return await _context.JobSeekerSkills.FirstOrDefaultAsync(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
        }

        /// <summary>
        /// Create a new job seeker skill asynchronously.
        /// </summary>
        /// <param name="jobSeekerSkill">The job seeker skill object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job seeker skill.</returns>
        public async Task<JobSeekerSkillsModel> CreateJobSeekerSkillAsync(JobSeekerSkillsModel jobSeekerSkill)
        {
            jobSeekerSkill.JobSeekerSkillID = 0; // EF Core will auto-generate the ID
            _context.JobSeekerSkills.Add(jobSeekerSkill);
            await _context.SaveChangesAsync();
            return jobSeekerSkill;
        }

        /// <summary>
        /// Update an existing job seeker skill asynchronously.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to update.</param>
        /// <param name "jobSeekerSkill">The updated job seeker skill object.</param>
        /// <returns>An asynchronous operation to update the job seeker skill.</returns>
        public async Task UpdateJobSeekerSkillAsync(int jobSeekerSkillID, JobSeekerSkillsModel jobSeekerSkill)
        {
            var existingJobSeekerSkill = await _context.JobSeekerSkills.FirstOrDefaultAsync(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
            if (existingJobSeekerSkill != null)
            {
                existingJobSeekerSkill.JobSeekerID = jobSeekerSkill.JobSeekerID;
                existingJobSeekerSkill.SkillID = jobSeekerSkill.SkillID;
                existingJobSeekerSkill.ProficiencyLevel = jobSeekerSkill.ProficiencyLevel;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a job seeker skill by its unique ID asynchronously.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to delete.</param>
        /// <returns>An asynchronous operation to delete the job seeker skill.</returns>
        public async Task DeleteJobSeekerSkillAsync(int jobSeekerSkillID)
        {
            var jobSeekerSkillToRemove = await _context.JobSeekerSkills.FirstOrDefaultAsync(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
            if (jobSeekerSkillToRemove != null)
            {
                _context.JobSeekerSkills.Remove(jobSeekerSkillToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
