using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class EmployersJobListingsService
    {
        private readonly List<EmployersJobListingsModel> _employersJobListings = new List<EmployersJobListingsModel>();
        private int _nextEmployersJobListingID = 1;

        /// <summary>
        /// Get a list of all employer's job listings.
        /// </summary>
        public IEnumerable<EmployersJobListingsModel> GetEmployersJobListings()
        {
            return _employersJobListings;
        }

        /// <summary>
        /// Get an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="employersJobListingID">The ID of the employer's job listing to retrieve.</param>
        public EmployersJobListingsModel GetEmployersJobListing(int employersJobListingID)
        {
            return _employersJobListings.FirstOrDefault(ejl => ejl.EmployersJobListingID == employersJobListingID);
        }

        /// <summary>
        /// Create a new employer's job listing.
        /// </summary>
        /// <param name="employersJobListing">The employer's job listing object to create.</param>
        public EmployersJobListingsModel CreateEmployersJobListing(EmployersJobListingsModel employersJobListing)
        {
            employersJobListing.EmployersJobListingID = _nextEmployersJobListingID++;
            _employersJobListings.Add(employersJobListing);
            return employersJobListing;
        }

        /// <summary>
        /// Delete an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="employersJobListingID">The ID of the employer's job listing to delete.</param>
        public void DeleteEmployersJobListing(int employersJobListingID)
        {
            var employerJobListingToRemove = _employersJobListings.FirstOrDefault(ejl => ejl.EmployersJobListingID == employersJobListingID);
            if (employerJobListingToRemove != null)
            {
                _employersJobListings.Remove(employerJobListingToRemove);
            }
        }
    }
}
