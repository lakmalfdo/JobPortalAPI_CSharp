using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class JobListingsService
    {
        private readonly List<JobListingsModel> _jobListings = new List<JobListingsModel>();
        private int _nextJobID = 1;

        /// <summary>
        /// Get a list of all job listings.
        /// </summary>
        public IEnumerable<JobListingsModel> GetJobListings()
        {
            return _jobListings;
        }

        /// <summary>
        /// Get a job listing by its unique ID.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to retrieve.</param>
        public JobListingsModel GetJobListing(int jobID)
        {
            return _jobListings.FirstOrDefault(j => j.JobID == jobID);
        }

        /// <summary>
        /// Create a new job listing.
        /// </summary>
        /// <param name="jobListing">The job listing object to create.</param>
        public JobListingsModel CreateJobListing(JobListingsModel jobListing)
        {
            jobListing.JobID = _nextJobID++;
            _jobListings.Add(jobListing);
            return jobListing;
        }

        /// <summary>
        /// Update an existing job listing.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to update.</param>
        /// <param name="jobListing">The updated job listing object.</param>
        public void UpdateJobListing(int jobID, JobListingsModel jobListing)
        {
            var existingJobListing = _jobListings.FirstOrDefault(j => j.JobID == jobID);
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
            }
        }

        /// <summary>
        /// Delete a job listing by its unique ID.
        /// </summary>
        /// <param name="jobID">The ID of the job listing to delete.</param>
        public void DeleteJobListing(int jobID)
        {
            var jobListingToRemove = _jobListings.FirstOrDefault(j => j.JobID == jobID);
            if (jobListingToRemove != null)
            {
                _jobListings.Remove(jobListingToRemove);
            }
        }
    }
}
