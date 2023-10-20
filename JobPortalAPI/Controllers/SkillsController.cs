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

        public SkillsController(SkillsService skillsService)
        {
            _skillsService = skillsService;
        }

        /// <summary>
        /// Get a list of all skills.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<SkillsModel>> GetSkills()
        {
            return Ok(_skillsService.GetSkills());
        }

        /// <summary>
        /// Get a skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the skill to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<SkillsModel> GetSkill(int id)
        {
            var skill = _skillsService.GetSkill(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        /// <summary>
        /// Create a new skill.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        [HttpPost]
        public ActionResult<SkillsModel> CreateSkill(SkillsModel skill)
        {
            var createdSkill = _skillsService.CreateSkill(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = createdSkill.SkillID }, createdSkill);
        }

        /// <summary>
        /// Update an existing skill.
        /// </summary>
        /// <param name="id">The ID of the skill to update.</param>
        /// <param name="skill">The updated skill object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateSkill(int id, SkillsModel skill)
        {
            if (id != skill.SkillID)
            {
                return BadRequest();
            }

            _skillsService.UpdateSkill(id, skill);

            return NoContent();
        }

        /// <summary>
        /// Delete a skill by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the skill to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {
            _skillsService.DeleteSkill(id);
            return NoContent();
        }
    }
}
