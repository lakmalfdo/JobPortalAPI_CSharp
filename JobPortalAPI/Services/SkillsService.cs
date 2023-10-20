using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class SkillsService
    {
        private readonly List<SkillsModel> _skills = new List<SkillsModel>();
        private int _nextSkillID = 1;

        /// <summary>
        /// Get a list of all skills.
        /// </summary>
        public IEnumerable<SkillsModel> GetSkills()
        {
            return _skills;
        }

        /// <summary>
        /// Get a skill by its unique ID.
        /// </summary>
        /// <param name="skillID">The ID of the skill to retrieve.</param>
        public SkillsModel GetSkill(int skillID)
        {
            return _skills.FirstOrDefault(s => s.SkillID == skillID);
        }

        /// <summary>
        /// Create a new skill.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        public SkillsModel CreateSkill(SkillsModel skill)
        {
            skill.SkillID = _nextSkillID++;
            _skills.Add(skill);
            return skill;
        }

        /// <summary>
        /// Update an existing skill.
        /// </summary>
        /// <param name="skillID">The ID of the skill to update.</param>
        /// <param name="skill">The updated skill object.</param>
        public void UpdateSkill(int skillID, SkillsModel skill)
        {
            var existingSkill = _skills.FirstOrDefault(s => s.SkillID == skillID);
            if (existingSkill != null)
            {
                existingSkill.SkillName = skill.SkillName;
                existingSkill.SkillDescription = skill.SkillDescription;
            }
        }

        /// <summary>
        /// Delete a skill by its unique ID.
        /// </summary>
        /// <param name="skillID">The ID of the skill to delete.</param>
        public void DeleteSkill(int skillID)
        {
            var skillToRemove = _skills.FirstOrDefault(s => s.SkillID == skillID);
            if (skillToRemove != null)
            {
                _skills.Remove(skillToRemove);
            }
        }
    }
}
