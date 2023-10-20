using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/employersjoblistings")]
    [ApiController]
    public class EmployersJobListingsController : ControllerBase
    {
        private readonly EmployersJobListingsService _employersJobListingsService;

        public EmployersJobListingsController(EmployersJobListingsService employersJobListingsService)
        {
            _employersJobListingsService = employersJobListingsService;
        }

        /// <summary>
        /// Get a list of all employer's job listings.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<EmployersJobListingsModel>> GetEmployersJobListings()
        {
            return Ok(_employersJobListingsService.GetEmployersJobListings());
        }

        /// <summary>
        /// Get an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer's job listing to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<EmployersJobListingsModel> GetEmployersJobListing(int id)
        {
            var employerJobListing = _employersJobListingsService.GetEmployersJobListing(id);
            if (employerJobListing == null)
            {
                return NotFound();
            }
            return Ok(employerJobListing);
        }

        /// <summary>
        /// Create a new employer's job listing.
        /// </summary>
        /// <param name="employerJobListing">The employer's job listing object to create.</param>
        [HttpPost]
        public ActionResult<EmployersJobListingsModel> CreateEmployersJobListing(EmployersJobListingsModel employerJobListing)
        {
            var createdEmployerJobListing = _employersJobListingsService.CreateEmployersJobListing(employerJobListing);
            return CreatedAtAction(nameof(GetEmployersJobListing), new { id = createdEmployerJobListing.EmployersJobListingID }, createdEmployerJobListing);
        }

        /// <summary>
        /// Delete an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer's job listing to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployersJobListing(int id)
        {
            _employersJobListingsService.DeleteEmployersJobListing(id);
            return NoContent();
        }
    }
}
