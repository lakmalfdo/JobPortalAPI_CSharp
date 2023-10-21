using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/jobseekerskills")]
    [ApiController]
    public class JobSeekerSkillsController : ControllerBase
    {
        private readonly JobSeekerSkillsService _jobSeekerSkillsService;
        private readonly ILogger<JobSeekerSkillsController> _logger;

        public JobSeekerSkillsController(JobSeekerSkillsService jobSeekerSkillsService, ILogger<JobSeekerSkillsController> logger)
        {
            _jobSeekerSkillsService = jobSeekerSkillsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all job seeker skills.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobSeekerSkillsModel>>>GetJobSeekerSkills()
        {
            try
            {
                var jobSeekerSkills = await _jobSeekerSkillsService.GetJobSeekerSkillsAsync();
                return Ok(jobSeekerSkills);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekerSkillsController)}.{nameof(GetJobSeekerSkills)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching job seeker skills.");
            }
        }

        /// <summary>
        /// Get a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobSeekerSkillsModel>> GetJobSeekerSkill(int id)
        {
            try
            {
                var jobSeekerSkill = await _jobSeekerSkillsService.GetJobSeekerSkillAsync(id);
                if (jobSeekerSkill == null)
                {
                    return NotFound();
                }
                return Ok(jobSeekerSkill);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekerSkillsController)}.{nameof(GetJobSeekerSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the job seeker skill.");
            }
        }

        /// <summary>
        /// Create a new job seeker skill.
        /// </summary>
        /// <param name="jobSeekerSkill">The job seeker skill object to create.</param>
        [HttpPost]
        public async Task<ActionResult<JobSeekerSkillsModel>> CreateJobSeekerSkill(JobSeekerSkillsModel jobSeekerSkill)
        {
            try
            {
                var createdJobSeekerSkill = await _jobSeekerSkillsService.CreateJobSeekerSkillAsync(jobSeekerSkill);
                return CreatedAtAction(nameof(GetJobSeekerSkill), new { id = createdJobSeekerSkill.JobSeekerSkillID }, createdJobSeekerSkill);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekerSkillsController)}.{nameof(CreateJobSeekerSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the job seeker skill.");
            }
        }

        /// <summary>
        /// Update an existing job seeker skill.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to update.</param>
        /// <param name="jobSeekerSkill">The updated job seeker skill object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobSeekerSkill(int id, JobSeekerSkillsModel jobSeekerSkill)
        {
            try
            {
                if (id != jobSeekerSkill.JobSeekerSkillID)
                {
                    return BadRequest();
                }

                await _jobSeekerSkillsService.UpdateJobSeekerSkillAsync(id, jobSeekerSkill);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekerSkillsController)}.{nameof(UpdateJobSeekerSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the job seeker skill.");
            }
        }

        /// <summary>
        /// Delete a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeekerSkill(int id)
        {
            try
            {
                await _jobSeekerSkillsService.DeleteJobSeekerSkillAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(JobSeekerSkillsController)}.{nameof(DeleteJobSeekerSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the job seeker skill.");
            }
        }
    }
}
