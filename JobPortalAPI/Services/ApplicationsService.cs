using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing job applications using Entity Framework and an injected DbContext.
    /// </summary>
    public class ApplicationsService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all job applications asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of job applications.</returns>
        public async Task<IEnumerable<ApplicationsModel>> GetApplicationsAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        /// <summary>
        /// Get a job application by its unique ID asynchronously.
        /// </summary>
        /// <param name="applicationID">The ID of the application to retrieve.</param>
        /// <returns>An asynchronous operation that returns the job application with the specified ID.</returns>
        public async Task<ApplicationsModel?> GetApplicationAsync(int applicationID)
        {
            return await _context.Applications.FirstOrDefaultAsync(app => app.ApplicationID == applicationID);
        }

        /// <summary>
        /// Create a new job application asynchronously.
        /// </summary>
        /// <param name="application">The application object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created job application.</returns>
        public async Task<ApplicationsModel> CreateApplicationAsync(ApplicationsModel application)
        {
            application.ApplicationID = _context.Applications.Max(a => a.ApplicationID) + 1;
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        /// <summary>
        /// Update an existing job application asynchronously.
        /// </summary>
        /// <param name="applicationID">The ID of the application to update.</param>
        /// <param name="application">The updated application object.</param>
        /// <returns>An asynchronous operation to update the job application.</returns>
        public async Task UpdateApplicationAsync(int applicationID, ApplicationsModel application)
        {
            var existingApplication = await _context.Applications.FirstOrDefaultAsync(app => app.ApplicationID == applicationID);
            if (existingApplication != null)
            {
                existingApplication.JobID = application.JobID;
                existingApplication.JobSeekerID = application.JobSeekerID;
                existingApplication.ApplicationStatus = application.ApplicationStatus;
                existingApplication.ApplicationDate = application.ApplicationDate;
                existingApplication.AttachedDocuments = application.AttachedDocuments;
                existingApplication.Comments = application.Comments;

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a job application by its unique ID asynchronously.
        /// </summary>
        /// <param name="applicationID">The ID of the application to delete.</param>
        /// <returns>An asynchronous operation to delete the job application.</returns>
        public async Task DeleteApplicationAsync(int applicationID)
        {
            var applicationToRemove = await _context.Applications.FirstOrDefaultAsync(app => app.ApplicationID == applicationID);
            if (applicationToRemove is not null)
            {
                _context.Applications.Remove(applicationToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
