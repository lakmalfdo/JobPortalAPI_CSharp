using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/employers")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly EmployersService _employersService;
        private readonly ILogger<EmployersController> _logger;

        public EmployersController(EmployersService employersService, ILogger<EmployersController> logger)
        {
            _employersService = employersService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all employers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployersModel>>>GetEmployers()
        {
            try
            {
                var employers = await _employersService.GetEmployersAsync();
                return Ok(employers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersController)}.{nameof(GetEmployers)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching employers.");
            }
        }

        /// <summary>
        /// Get an employer by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployersModel>> GetEmployer(int id)
        {
            try
            {
                var employer = await _employersService.GetEmployerAsync(id);
                if (employer == null)
                {
                    return NotFound();
                }
                return Ok(employer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersController)}.{nameof(GetEmployer)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the employer.");
            }
        }

        /// <summary>
        /// Create a new employer.
        /// </summary>
        /// <param name="employer">The employer object to create.</param>
        [HttpPost]
        public async Task<ActionResult<EmployersModel>> CreateEmployer(EmployersModel employer)
        {
            try
            {
                var createdEmployer = await _employersService.CreateEmployerAsync(employer);
                return CreatedAtAction(nameof(GetEmployer), new { id = createdEmployer.EmployerID }, createdEmployer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersController)}.{nameof(CreateEmployer)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the employer.");
            }
        }

        /// <summary>
        /// Update an existing employer.
        /// </summary>
        /// <param name="id">The ID of the employer to update.</param>
        /// <param name="employer">The updated employer object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, EmployersModel employer)
        {
            try
            {
                if (id != employer.EmployerID)
                {
                    return BadRequest();
                }

                await _employersService.UpdateEmployerAsync(id, employer);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersController)}.{nameof(UpdateEmployer)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the employer.");
            }
        }

        /// <summary>
        /// Delete an employer by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            try
            {
                await _employersService.DeleteEmployerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmployersController)}.{nameof(DeleteEmployer)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the employer.");
            }
        }
    }
}
