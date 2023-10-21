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
        private readonly ILogger<JobCategoryMappingController> _logger;

        public JobCategoryMappingController(JobCategoryMappingService jobCategoryMappingService, ILogger<JobCategoryMappingController> logger)
        {
            _jobCategoryMappingService = jobCategoryMappingService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all job category mappings.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryMappingModel>>> GetJobCategoryMappings()
        {
            try
            {
                var jobCategoryMappings = await _jobCategoryMappingService.GetJobCategoryMappingsAsync();
                return Ok(jobCategoryMappings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobCategoryMappingController)}.{nameof(GetJobCategoryMappings)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching job category mappings.");
            }
        }

        /// <summary>
        /// Get a job category mapping by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job category mapping to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategoryMappingModel>> GetJobCategoryMapping(int id)
        {
            try
            {
                var jobCategoryMapping = await _jobCategoryMappingService.GetJobCategoryMappingAsync(id);
                if (jobCategoryMapping == null)
                {
                    return NotFound();
                }
                return Ok(jobCategoryMapping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobCategoryMappingController)}.{nameof(GetJobCategoryMapping)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the job category mapping.");
            }
        }

        /// <summary>
        /// Create a new job category mapping.
        /// </summary>
        /// <param name="jobCategoryMapping">The job category mapping object to create.</param>
        [HttpPost]
        public async Task<ActionResult<JobCategoryMappingModel>> CreateJobCategoryMapping(JobCategoryMappingModel jobCategoryMapping)
        {
            try
            {
                var createdJobCategoryMapping = await _jobCategoryMappingService.CreateJobCategoryMappingAsync(jobCategoryMapping);
                return CreatedAtAction(nameof(GetJobCategoryMapping), new { id = createdJobCategoryMapping.JobCategoryMappingID }, createdJobCategoryMapping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobCategoryMappingController)}.{nameof(CreateJobCategoryMapping)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the job category mapping.");
            }
        }

        /// <summary>
        /// Delete a job category mapping by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job category mapping to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCategoryMapping(int id)
        {
            try
            {
                await _jobCategoryMappingService.DeleteJobCategoryMappingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobCategoryMappingController)}.{nameof(DeleteJobCategoryMapping)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the job category mapping.");
            }
        }
    }
}
