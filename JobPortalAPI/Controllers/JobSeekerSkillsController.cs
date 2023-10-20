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

        public JobSeekerSkillsController(JobSeekerSkillsService jobSeekerSkillsService)
        {
            _jobSeekerSkillsService = jobSeekerSkillsService;
        }

        /// <summary>
        /// Get a list of all job seeker skills.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<JobSeekerSkillsModel>> GetJobSeekerSkills()
        {
            return Ok(_jobSeekerSkillsService.GetJobSeekerSkills());
        }

        /// <summary>
        /// Get a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<JobSeekerSkillsModel> GetJobSeekerSkill(int id)
        {
            var jobSeekerSkill = _jobSeekerSkillsService.GetJobSeekerSkill(id);
            if (jobSeekerSkill == null)
            {
                return NotFound();
            }
            return Ok(jobSeekerSkill);
        }

        /// <summary>
        /// Create a new job seeker skill.
        /// </summary>
        /// <param name="jobSeekerSkill">The job seeker skill object to create.</param>
        [HttpPost]
        public ActionResult<JobSeekerSkillsModel> CreateJobSeekerSkill(JobSeekerSkillsModel jobSeekerSkill)
        {
            var createdJobSeekerSkill = _jobSeekerSkillsService.CreateJobSeekerSkill(jobSeekerSkill);
            return CreatedAtAction(nameof(GetJobSeekerSkill), new { id = createdJobSeekerSkill.JobSeekerSkillID }, createdJobSeekerSkill);
        }

        /// <summary>
        /// Update an existing job seeker skill.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to update.</param>
        /// <param name="jobSeekerSkill">The updated job seeker skill object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateJobSeekerSkill(int id, JobSeekerSkillsModel jobSeekerSkill)
        {
            if (id != jobSeekerSkill.JobSeekerSkillID)
            {
                return BadRequest();
            }

            _jobSeekerSkillsService.UpdateJobSeekerSkill(id, jobSeekerSkill);

            return NoContent();
        }

        /// <summary>
        /// Delete a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the job seeker skill to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteJobSeekerSkill(int id)
        {
            _jobSeekerSkillsService.DeleteJobSeekerSkill(id);
            return NoContent();
        }
    }
}
