using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/jobseekers")]
    [ApiController]
    public class JobSeekersController : ControllerBase
    {
        private readonly JobSeekersService _jobSeekersService;
        private readonly ILogger<JobSeekersController> _logger;

        public JobSeekersController(JobSeekersService jobSeekersService, ILogger<JobSeekersController> logger)
        {
            _jobSeekersService = jobSeekersService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all job seekers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobSeekersModel>>>GetJobSeekers()
        {
            try
            {
                var jobSeekers = await _jobSeekersService.GetJobSeekersAsync();
                return Ok(jobSeekers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekersController)}.{nameof(GetJobSeekers)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching job seekers.");
            }
        }

        /// <summary>
        /// Get a job seeker by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobSeekersModel>> GetJobSeeker(int id)
        {
            try
            {
                var jobSeeker = await _jobSeekersService.GetJobSeekerAsync(id);
                if (jobSeeker == null)
                {
                    return NotFound();
                }
                return Ok(jobSeeker);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekersController)}.{nameof(GetJobSeeker)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the job seeker.");
            }
        }

        /// <summary>
        /// Create a new job seeker.
        /// </summary>
        /// <param name="jobSeeker">The job seeker object to create.</param>
        [HttpPost]
        public async Task<ActionResult<JobSeekersModel>> CreateJobSeeker(JobSeekersModel jobSeeker)
        {
            try
            {
                var createdJobSeeker = await _jobSeekersService.CreateJobSeekerAsync(jobSeeker);
                return CreatedAtAction(nameof(GetJobSeeker), new { id = createdJobSeeker.JobSeekerID }, createdJobSeeker);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekersController)}.{nameof(CreateJobSeeker)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the job seeker.");
            }
        }

        /// <summary>
        /// Update an existing job seeker.
        /// </summary>
        /// <param name="id">The ID of the job seeker to update.</param>
        /// <param name="jobSeeker">The updated job seeker object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobSeeker(int id, JobSeekersModel jobSeeker)
        {
            try
            {
                if (id != jobSeeker.JobSeekerID)
                {
                    return BadRequest();
                }

                await _jobSeekersService.UpdateJobSeekerAsync(id, jobSeeker);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekersController)}.{nameof(UpdateJobSeeker)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the job seeker.");
            }
        }

        /// <summary>
        /// Delete a job seeker by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            try
            {
                await _jobSeekersService.DeleteJobSeekerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekersController)}.{nameof(DeleteJobSeeker)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the job seeker.");
            }
        }
    }
}
