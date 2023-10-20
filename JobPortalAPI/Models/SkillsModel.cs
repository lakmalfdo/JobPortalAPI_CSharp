using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class SkillsModel
    {
        public int SkillID { get; set; }

        [Required]
        public string SkillName { get; set; }

        public string SkillDescription { get; set; }
    }
}
