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

        public JobListingsController(JobListingsService jobListingsService)
        {
            _jobListingsService = jobListingsService;
        }

        /// <summary>
        /// Get a list of all job listings.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<JobListingsModel>> GetJobListings()
        {
            return Ok(_jobListingsService.GetJobListings());
        }

        /// <summary>
        /// Get a job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job listing to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<JobListingsModel> GetJobListing(int id)
        {
            var jobListing = _jobListingsService.GetJobListing(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            return Ok(jobListing);
        }

        /// <summary>
        /// Create a new job listing.
        /// </summary>
        /// <param name="jobListing">The job listing object to create.</param>
        [HttpPost]
        public ActionResult<JobListingsModel> CreateJobListing(JobListingsModel jobListing)
        {
            var createdJobListing = _jobListingsService.CreateJobListing(jobListing);
            return CreatedAtAction(nameof(GetJobListing), new { id = createdJobListing.JobID }, createdJobListing);
        }

        /// <summary>
        /// Update an existing job listing.
        /// </summary>
        /// <param name="id">The ID of the job listing to update.</param>
        /// <param name="jobListing">The updated job listing object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateJobListing(int id, JobListingsModel jobListing)
        {
            if (id != jobListing.JobID)
            {
                return BadRequest();
            }

            _jobListingsService.UpdateJobListing(id, jobListing);

            return NoContent();
        }

        /// <summary>
        /// Delete a job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job listing to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteJobListing(int id)
        {
            _jobListingsService.DeleteJobListing(id);
            return NoContent();
        }
    }
}
