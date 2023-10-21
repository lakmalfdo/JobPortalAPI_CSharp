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
        private readonly ILogger<EmployersJobListingsController> _logger;

        public EmployersJobListingsController(EmployersJobListingsService employersJobListingsService, ILogger<EmployersJobListingsController> logger)
        {
            _employersJobListingsService = employersJobListingsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all employer's job listings.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployersJobListingsModel>>>GetEmployersJobListings()
        {
            try
            {
                var employerJobListings = await _employersJobListingsService.GetEmployersJobListingsAsync();
                return Ok(employerJobListings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersJobListingsController)}.{nameof(GetEmployersJobListings)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching employer's job listings.");
            }
        }

        /// <summary>
        /// Get an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer's job listing to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployersJobListingsModel>> GetEmployersJobListing(int id)
        {
            try
            {
                var employerJobListing = await _employersJobListingsService.GetEmployersJobListingAsync(id);
                if (employerJobListing == null)
                {
                    return NotFound();
                }
                return Ok(employerJobListing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersJobListingsController)}.{nameof(GetEmployersJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the employer's job listing.");
            }
        }

        /// <summary>
        /// Create a new employer's job listing.
        /// </summary>
        /// <param name="employerJobListing">The employer's job listing object to create.</param>
        [HttpPost]
        public async Task<ActionResult<EmployersJobListingsModel>> CreateEmployersJobListing(EmployersJobListingsModel employerJobListing)
        {
            try
            {
                var createdEmployerJobListing = await _employersJobListingsService.CreateEmployersJobListingAsync(employerJobListing);
                return CreatedAtAction(nameof(GetEmployersJobListing), new { id = createdEmployerJobListing.EmployersJobListingID }, createdEmployerJobListing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersJobListingsController)}.{nameof(CreateEmployersJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the employer's job listing.");
            }
        }

        /// <summary>
        /// Delete an employer's job listing by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer's job listing to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployersJobListing(int id)
        {
            try
            {
                await _employersJobListingsService.DeleteEmployersJobListingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersJobListingsController)}.{nameof(DeleteEmployersJobListing)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the employer's job listing.");
            }
        }
    }
}
