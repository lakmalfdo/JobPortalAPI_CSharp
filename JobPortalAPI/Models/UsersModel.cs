using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class UsersModel
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        public string ProfilePicture { get; set; }

        public string AuthenticationToken { get; set; }
    }

    public enum UserRole
    {
        Employer,
        JobSeeker
    }
}
