using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class CategoriesModel
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
    }
}
