using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Models;

namespace JobPortalAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<EmployersModel> Employers { get; set; }
        public DbSet<JobSeekersModel> JobSeekers { get; set; }
        public DbSet<JobListingsModel> JobListings { get; set; }
        public DbSet<JobCategoryMappingModel> JobCategoryMappings { get; set; }
        public DbSet<MessagesModel> Messages { get; set; }
        public DbSet<NotificationsModel> Notifications { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<ApplicationsModel> Applications { get; set; }
        public DbSet<SkillsModel> Skills { get; set; }
        public DbSet<JobSeekerSkillsModel> JobSeekerSkills { get; set; }
        public DbSet<EmployersJobListingsModel> EmployersJobListings { get; set; }
    }
}
