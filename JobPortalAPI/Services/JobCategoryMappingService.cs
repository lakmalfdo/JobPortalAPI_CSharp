using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class JobCategoryMappingService
    {
        private readonly List<JobCategoryMappingModel> _jobCategoryMappings = new List<JobCategoryMappingModel>();
        private int _nextJobCategoryMappingID = 1;

        /// <summary>
        /// Get a list of all job category mappings.
        /// </summary>
        public IEnumerable<JobCategoryMappingModel> GetJobCategoryMappings()
        {
            return _jobCategoryMappings;
        }

        /// <summary>
        /// Get a job category mapping by its unique ID.
        /// </summary>
        /// <param name="jobCategoryMappingID">The ID of the job category mapping to retrieve.</param>
        public JobCategoryMappingModel GetJobCategoryMapping(int jobCategoryMappingID)
        {
            return _jobCategoryMappings.FirstOrDefault(jcm => jcm.JobCategoryMappingID == jobCategoryMappingID);
        }

        /// <summary>
        /// Create a new job category mapping.
        /// </summary>
        /// <param name="jobCategoryMapping">The job category mapping object to create.</param>
        public JobCategoryMappingModel CreateJobCategoryMapping(JobCategoryMappingModel jobCategoryMapping)
        {
            jobCategoryMapping.JobCategoryMappingID = _nextJobCategoryMappingID++;
            _jobCategoryMappings.Add(jobCategoryMapping);
            return jobCategoryMapping;
        }

        /// <summary>
        /// Delete a job category mapping by its unique ID.
        /// </summary>
        /// <param name="jobCategoryMappingID">The ID of the job category mapping to delete.</param>
        public void DeleteJobCategoryMapping(int jobCategoryMappingID)
        {
            var jobCategoryMappingToRemove = _jobCategoryMappings.FirstOrDefault(jcm => jcm.JobCategoryMappingID == jobCategoryMappingID);
            if (jobCategoryMappingToRemove != null)
            {
                _jobCategoryMappings.Remove(jobCategoryMappingToRemove);
            }
        }
    }
}
