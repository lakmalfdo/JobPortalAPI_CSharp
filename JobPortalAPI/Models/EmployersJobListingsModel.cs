using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class EmployersJobListingsModel
    {
        public int EmployersJobListingID { get; set; }
        public int EmployerID { get; set; }
        public int JobID { get; set; }
    }
}
