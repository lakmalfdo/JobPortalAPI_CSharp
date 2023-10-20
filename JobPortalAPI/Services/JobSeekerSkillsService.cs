using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class JobSeekerSkillsService
    {
        private readonly List<JobSeekerSkillsModel> _jobSeekerSkills = new List<JobSeekerSkillsModel>();
        private int _nextJobSeekerSkillID = 1;

        /// <summary>
        /// Get a list of all job seeker skills.
        /// </summary>
        public IEnumerable<JobSeekerSkillsModel> GetJobSeekerSkills()
        {
            return _jobSeekerSkills;
        }

        /// <summary>
        /// Get a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to retrieve.</param>
        public JobSeekerSkillsModel GetJobSeekerSkill(int jobSeekerSkillID)
        {
            return _jobSeekerSkills.FirstOrDefault(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
        }

        /// <summary>
        /// Create a new job seeker skill.
        /// </summary>
        /// <param name="jobSeekerSkill">The job seeker skill object to create.</param>
        public JobSeekerSkillsModel CreateJobSeekerSkill(JobSeekerSkillsModel jobSeekerSkill)
        {
            jobSeekerSkill.JobSeekerSkillID = _nextJobSeekerSkillID++;
            _jobSeekerSkills.Add(jobSeekerSkill);
            return jobSeekerSkill;
        }

        /// <summary>
        /// Update an existing job seeker skill.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to update.</param>
        /// <param name="jobSeekerSkill">The updated job seeker skill object.</param>
        public void UpdateJobSeekerSkill(int jobSeekerSkillID, JobSeekerSkillsModel jobSeekerSkill)
        {
            var existingJobSeekerSkill = _jobSeekerSkills.FirstOrDefault(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
            if (existingJobSeekerSkill != null)
            {
                existingJobSeekerSkill.JobSeekerID = jobSeekerSkill.JobSeekerID;
                existingJobSeekerSkill.SkillID = jobSeekerSkill.SkillID;
                existingJobSeekerSkill.ProficiencyLevel = jobSeekerSkill.ProficiencyLevel;
            }
        }

        /// <summary>
        /// Delete a job seeker skill by its unique ID.
        /// </summary>
        /// <param name="jobSeekerSkillID">The ID of the job seeker skill to delete.</param>
        public void DeleteJobSeekerSkill(int jobSeekerSkillID)
        {
            var jobSeekerSkillToRemove = _jobSeekerSkills.FirstOrDefault(jss => jss.JobSeekerSkillID == jobSeekerSkillID);
            if (jobSeekerSkillToRemove != null)
            {
                _jobSeekerSkills.Remove(jobSeekerSkillToRemove);
            }
        }
    }
}
