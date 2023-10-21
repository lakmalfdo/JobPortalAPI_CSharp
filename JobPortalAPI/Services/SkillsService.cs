using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing skills using Entity Framework and an injected DbContext.
    /// </summary>
    public class SkillsService
    {
        private readonly ApplicationDbContext _context;

        public SkillsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all skills asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of skills.</returns>
        public async Task<IEnumerable<SkillsModel>> GetSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        /// <summary>
        /// Get a skill by its unique ID asynchronously.
        /// </summary>
        /// <param name="skillID">The ID of the skill to retrieve.</param>
        /// <returns>An asynchronous operation that returns the skill with the specified ID.</returns>
        public async Task<SkillsModel?> GetSkillAsync(int skillID)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.SkillID == skillID);
        }

        /// <summary>
        /// Create a new skill asynchronously.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created skill.</returns>
        public async Task<SkillsModel> CreateSkillAsync(SkillsModel skill)
        {
            skill.SkillID = 0; // EF Core will auto-generate the ID
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        /// <summary>
        /// Update an existing skill asynchronously.
        /// </summary>
        /// <param name="skillID">The ID of the skill to update.</param>
        /// <param name="skill">The updated skill object.</param>
        /// <returns>An asynchronous operation to update the skill.</returns>
        public async Task UpdateSkillAsync(int skillID, SkillsModel skill)
        {
            var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillID == skillID);
            if (existingSkill != null)
            {
                existingSkill.SkillName = skill.SkillName;
                existingSkill.SkillDescription = skill.SkillDescription;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a skill by its unique ID asynchronously.
        /// </summary>
        /// <param name="skillID">The ID of the skill to delete.</param>
        /// <returns>An asynchronous operation to delete the skill.</returns>
        public async Task DeleteSkillAsync(int skillID)
        {
            var skillToRemove = await _context.Skills.FirstOrDefaultAsync(s => s.SkillID == skillID);
            if (skillToRemove != null)
            {
                _context.Skills.Remove(skillToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
