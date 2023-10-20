using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class EmployersModel
    {
        public int EmployerID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyLogo { get; set; }
    }
}
