using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class ApplicationsModel
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int JobSeekerID { get; set; }

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }

        public string AttachedDocuments { get; set; }

        public string Comments { get; set; }
    }

    public enum ApplicationStatus
    {
        Pending,
        Reviewed,
        Rejected,
        Accepted,
        Other
    }
}
