using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class JobSeekersService
    {
        private readonly List<JobSeekersModel> _jobSeekers = new List<JobSeekersModel>();
        private int _nextJobSeekerID = 1;

        /// <summary>
        /// Get a list of all job seekers.
        /// </summary>
        public IEnumerable<JobSeekersModel> GetJobSeekers()
        {
            return _jobSeekers;
        }

        /// <summary>
        /// Get a job seeker by their unique ID.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to retrieve.</param>
        public JobSeekersModel GetJobSeeker(int jobSeekerID)
        {
            return _jobSeekers.FirstOrDefault(js => js.JobSeekerID == jobSeekerID);
        }

        /// <summary>
        /// Create a new job seeker.
        /// </summary>
        /// <param name="jobSeeker">The job seeker object to create.</param>
        public JobSeekersModel CreateJobSeeker(JobSeekersModel jobSeeker)
        {
            jobSeeker.JobSeekerID = _nextJobSeekerID++;
            _jobSeekers.Add(jobSeeker);
            return jobSeeker;
        }

        /// <summary>
        /// Update an existing job seeker.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to update.</param>
        /// <param name="jobSeeker">The updated job seeker object.</param>
        public void UpdateJobSeeker(int jobSeekerID, JobSeekersModel jobSeeker)
        {
            var existingJobSeeker = _jobSeekers.FirstOrDefault(js => js.JobSeekerID == jobSeekerID);
            if (existingJobSeeker != null)
            {
                existingJobSeeker.Resume = jobSeeker.Resume;
                existingJobSeeker.CoverLetter = jobSeeker.CoverLetter;
            }
        }

        /// <summary>
        /// Delete a job seeker by their unique ID.
        /// </summary>
        /// <param name="jobSeekerID">The ID of the job seeker to delete.</param>
        public void DeleteJobSeeker(int jobSeekerID)
        {
            var jobSeekerToRemove = _jobSeekers.FirstOrDefault(js => js.JobSeekerID == jobSeekerID);
            if (jobSeekerToRemove != null)
            {
                _jobSeekers.Remove(jobSeekerToRemove);
            }
        }
    }
}
