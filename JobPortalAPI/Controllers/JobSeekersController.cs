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

        public JobSeekersController(JobSeekersService jobSeekersService)
        {
            _jobSeekersService = jobSeekersService;
        }

        /// <summary>
        /// Get a list of all job seekers.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<JobSeekersModel>> GetJobSeekers()
        {
            return Ok(_jobSeekersService.GetJobSeekers());
        }

        /// <summary>
        /// Get a job seeker by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<JobSeekersModel> GetJobSeeker(int id)
        {
            var jobSeeker = _jobSeekersService.GetJobSeeker(id);
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return Ok(jobSeeker);
        }

        /// <summary>
        /// Create a new job seeker.
        /// </summary>
        /// <param name="jobSeeker">The job seeker object to create.</param>
        [HttpPost]
        public ActionResult<JobSeekersModel> CreateJobSeeker(JobSeekersModel jobSeeker)
        {
            var createdJobSeeker = _jobSeekersService.CreateJobSeeker(jobSeeker);
            return CreatedAtAction(nameof(GetJobSeeker), new { id = createdJobSeeker.JobSeekerID }, createdJobSeeker);
        }

        /// <summary>
        /// Update an existing job seeker.
        /// </summary>
        /// <param name="id">The ID of the job seeker to update.</param>
        /// <param name="jobSeeker">The updated job seeker object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateJobSeeker(int id, JobSeekersModel jobSeeker)
        {
            if (id != jobSeeker.JobSeekerID)
            {
                return BadRequest();
            }

            _jobSeekersService.UpdateJobSeeker(id, jobSeeker);

            return NoContent();
        }

        /// <summary>
        /// Delete a job seeker by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteJobSeeker(int id)
        {
            _jobSeekersService.DeleteJobSeeker(id);
            return NoContent();
        }
    }
}
