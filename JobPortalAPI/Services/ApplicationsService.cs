using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class ApplicationsService
    {
        private readonly List<ApplicationsModel> _applications = new List<ApplicationsModel>();
        private int _nextApplicationID = 1;

        /// <summary>
        /// Get a list of all applications.
        /// </summary>
        public IEnumerable<ApplicationsModel> GetApplications()
        {
            return _applications;
        }

        /// <summary>
        /// Get an application by its unique ID.
        /// </summary>
        /// <param name="applicationID">The ID of the application to retrieve.</param>
        public ApplicationsModel GetApplication(int applicationID)
        {
            return _applications.FirstOrDefault(app => app.ApplicationID == applicationID);
        }

        /// <summary>
        /// Create a new application.
        /// </summary>
        /// <param name="application">The application object to create.</param>
        public ApplicationsModel CreateApplication(ApplicationsModel application)
        {
            application.ApplicationID = _nextApplicationID++;
            _applications.Add(application);
            return application;
        }

        /// <summary>
        /// Update an existing application.
        /// </summary>
        /// <param name="applicationID">The ID of the application to update.</param>
        /// <param name="application">The updated application object.</param>
        public void UpdateApplication(int applicationID, ApplicationsModel application)
        {
            var existingApplication = _applications.FirstOrDefault(app => app.ApplicationID == applicationID);
            if (existingApplication != null)
            {
                existingApplication.JobID = application.JobID;
                existingApplication.JobSeekerID = application.JobSeekerID;
                existingApplication.ApplicationStatus = application.ApplicationStatus;
                existingApplication.ApplicationDate = application.ApplicationDate;
                existingApplication.AttachedDocuments = application.AttachedDocuments;
                existingApplication.Comments = application.Comments;
            }
        }

        /// <summary>
        /// Delete an application by its unique ID.
        /// </summary>
        /// <param name="applicationID">The ID of the application to delete.</param>
        public void DeleteApplication(int applicationID)
        {
            var applicationToRemove = _applications.FirstOrDefault(app => app.ApplicationID == applicationID);
            if (applicationToRemove != null)
            {
                _applications.Remove(applicationToRemove);
            }
        }
    }
}
