using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/joblistings")]
    [ApiController]
    public class JobListingsController : ControllerBase
    {
        private readonly JobListingsService _jobListingsService;
        private readonly ILogger<JobListingsController> _logger;

        public JobListingsController(JobListingsService jobListingsService, ILogger<JobListingsController> logger)
        {
            _jobListingsService = jobListingsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all job listings.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobListingsModel>>> GetJobListings()
        {
            try
            {
                var jobListings = await _jobListingsService.GetJobListingsAsync();
                return Ok(jobListings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobListingsController)}.{nameof(GetJobListings)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching job listings.");
            }
        }

        /// <summary>
        /// Get a job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job listing to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobListingsModel>> GetJobListing(int id)
        {
            try
            {
                var jobListing = await _jobListingsService.GetJobListingAsync(id);
                if (jobListing == null)
                {
                    return NotFound();
                }
                return Ok(jobListing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobListingsController)}.{nameof(GetJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the job listing.");
            }
        }

        /// <summary>
        /// Create a new job listing.
        /// </summary>
        /// <param name="jobListing">The job listing object to create.</param>
        [HttpPost]
        public async Task<ActionResult<JobListingsModel>> CreateJobListing(JobListingsModel jobListing)
        {
            try
            {
                var createdJobListing = await _jobListingsService.CreateJobListingAsync(jobListing);
                return CreatedAtAction(nameof(GetJobListing), new { id = createdJobListing.JobID }, createdJobListing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobListingsController)}.{nameof(CreateJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the job listing.");
            }
        }

        /// <summary>
        /// Update an existing job listing.
        /// </summary>
        /// <param name="id">The ID of the job listing to update.</param>
        /// <param name="jobListing">The updated job listing object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobListing(int id, JobListingsModel jobListing)
        {
            try
            {
                if (id != jobListing.JobID)
                {
                    return BadRequest();
                }

                await _jobListingsService.UpdateJobListingAsync(id, jobListing);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobListingsController)}.{nameof(UpdateJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the job listing.");
            }
        }

        /// <summary>
        /// Delete a job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job listing to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobListing(int id)
        {
            try
            {
                await _jobListingsService.DeleteJobListingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobListingsController)}.{nameof(DeleteJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the job listing.");
            }
        }
    }
}
