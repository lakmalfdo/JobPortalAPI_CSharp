using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class JobSeekerSkillsModel
    {
        public int JobSeekerSkillID { get; set; }
        public int JobSeekerID { get; set; }
        public int SkillID { get; set; }

        [Required]
        public ProficiencyLevel ProficiencyLevel { get; set; }
    }

    public enum ProficiencyLevel
    {
        Beginner,
        Intermediate,
        Expert
    }
}
