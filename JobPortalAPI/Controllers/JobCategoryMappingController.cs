using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/jobcategorymapping")]
    [ApiController]
    public class JobCategoryMappingController : ControllerBase
    {
        private readonly JobCategoryMappingService _jobCategoryMappingService;

        public JobCategoryMappingController(JobCategoryMappingService jobCategoryMappingService)
        {
            _jobCategoryMappingService = jobCategoryMappingService;
        }

        /// <summary>
        /// Get a list of all job category mappings.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<JobCategoryMappingModel>> GetJobCategoryMappings()
        {
            return Ok(_jobCategoryMappingService.GetJobCategoryMappings());
        }

        /// <summary>
        /// Get a job category mapping by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job category mapping to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<JobCategoryMappingModel> GetJobCategoryMapping(int id)
        {
            var jobCategoryMapping = _jobCategoryMappingService.GetJobCategoryMapping(id);
            if (jobCategoryMapping == null)
            {
                return NotFound();
            }
            return Ok(jobCategoryMapping);
        }

        /// <summary>
        /// Create a new job category mapping.
        /// </summary>
        /// <param name="jobCategoryMapping">The job category mapping object to create.</param>
        [HttpPost]
        public ActionResult<JobCategoryMappingModel> CreateJobCategoryMapping(JobCategoryMappingModel jobCategoryMapping)
        {
            var createdJobCategoryMapping = _jobCategoryMappingService.CreateJobCategoryMapping(jobCategoryMapping);
            return CreatedAtAction(nameof(GetJobCategoryMapping), new { id = createdJobCategoryMapping.JobCategoryMappingID }, createdJobCategoryMapping);
        }

        /// <summary>
        /// Delete a job category mapping by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job category mapping to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteJobCategoryMapping(int id)
        {
            _jobCategoryMappingService.DeleteJobCategoryMapping(id);
            return NoContent();
        }
    }
}
