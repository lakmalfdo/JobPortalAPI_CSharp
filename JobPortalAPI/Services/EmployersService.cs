using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class EmployersService
    {
        private readonly List<EmployersModel> _employers = new List<EmployersModel>();
        private int _nextEmployerID = 1;

        /// <summary>
        /// Get a list of all employers.
        /// </summary>
        public IEnumerable<EmployersModel> GetEmployers()
        {
            return _employers;
        }

        /// <summary>
        /// Get an employer by their unique ID.
        /// </summary>
        /// <param name="employerID">The ID of the employer to retrieve.</param>
        public EmployersModel GetEmployer(int employerID)
        {
            return _employers.FirstOrDefault(e => e.EmployerID == employerID);
        }

        /// <summary>
        /// Create a new employer.
        /// </summary>
        /// <param name="employer">The employer object to create.</param>
        public EmployersModel CreateEmployer(EmployersModel employer)
        {
            employer.EmployerID = _nextEmployerID++;
            _employers.Add(employer);
            return employer;
        }

        /// <summary>
        /// Update an existing employer.
        /// </summary>
        /// <param name="employerID">The ID of the employer to update.</param>
        /// <param name="employer">The updated employer object.</param>
        public void UpdateEmployer(int employerID, EmployersModel employer)
        {
            var existingEmployer = _employers.FirstOrDefault(e => e.EmployerID == employerID);
            if (existingEmployer != null)
            {
                existingEmployer.CompanyName = employer.CompanyName;
                existingEmployer.CompanyDescription = employer.CompanyDescription;
                existingEmployer.CompanyLogo = employer.CompanyLogo;
            }
        }

        /// <summary>
        /// Delete an employer by their unique ID.
        /// </summary>
        /// <param name="employerID">The ID of the employer to delete.</param>
        public void DeleteEmployer(int employerID)
        {
            var employerToRemove = _employers.FirstOrDefault(e => e.EmployerID == employerID);
            if (employerToRemove != null)
            {
                _employers.Remove(employerToRemove);
            }
        }
    }
}
