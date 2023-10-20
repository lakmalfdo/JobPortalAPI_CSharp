using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class JobSeekersModel
    {
        public int JobSeekerID { get; set; }

        public string Resume { get; set; }

        public string CoverLetter { get; set; }
    }
}
