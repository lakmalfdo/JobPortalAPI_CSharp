using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly SkillsService _skillsService;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(SkillsService skillsService, ILogger<SkillsController> logger)
        {
            _skillsService = skillsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all skills.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillsModel>>>GetSkills()
        {
            try
            {
                var skills = await _skillsService.GetSkillsAsync();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SkillsController)}.{nameof(GetSkills)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching skills.");
            }
        }

        /// <summary>
        /// Get a skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the skill to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillsModel>> GetSkill(int id)
        {
            try
            {
                var skill = await _skillsService.GetSkillAsync(id);
                if (skill == null)
                {
                    return NotFound();
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SkillsController)}.{nameof(GetSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the skill.");
            }
        }

        /// <summary>
        /// Create a new skill.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        [HttpPost]
        public async Task<ActionResult<SkillsModel>> CreateSkill(SkillsModel skill)
        {
            try
            {
                var createdSkill = await _skillsService.CreateSkillAsync(skill);
                return CreatedAtAction(nameof(GetSkill), new { id = createdSkill.SkillID }, createdSkill);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SkillsController)}.{nameof(CreateSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the skill.");
            }
        }

        /// <summary>
        /// Update an existing skill.
        /// </summary>
        /// <param name="id">The ID of the skill to update.</param>
        /// <param name="skill">The updated skill object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, SkillsModel skill)
        {
            try
            {
                if (id != skill.SkillID)
                {
                    return BadRequest();
                }

                await _skillsService.UpdateSkillAsync(id, skill);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SkillsController)}.{nameof(UpdateSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the skill.");
            }
        }

        /// <summary>
        /// Delete a skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the skill to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                await _skillsService.DeleteSkillAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SkillsController)}.{nameof(DeleteSkill)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the skill.");
            }
        }
    }
}
