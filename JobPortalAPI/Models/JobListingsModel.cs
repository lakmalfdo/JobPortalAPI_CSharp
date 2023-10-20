using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class JobListingsModel
    {
        public int JobID { get; set; }
        public int EmployerID { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        public string JobRequirements { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal Salary { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ApplicationDeadline { get; set; }

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
