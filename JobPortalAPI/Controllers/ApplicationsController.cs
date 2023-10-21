using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationsService _applicationsService;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(ApplicationsService applicationsService, ILogger<ApplicationsController> logger)
        {
            _applicationsService = applicationsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all applications.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationsModel>>> GetApplications()
        {
            try
            {
                var applications = await _applicationsService.GetApplicationsAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ApplicationsController)}.{nameof(GetApplications)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching applications.");
            }
        }

        /// <summary>
        /// Get an application by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the application to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationsModel>> GetApplication(int id)
        {
            try
            {
                var application = await _applicationsService.GetApplicationAsync(id);
                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ApplicationsController)}.{nameof(GetApplication)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the application.");
            }
        }

        /// <summary>
        /// Create a new application.
        /// </summary>
        /// <param name="application">The application object to create.</param>
        [HttpPost]
        public async Task<ActionResult<ApplicationsModel>> CreateApplication(ApplicationsModel application)
        {
            try
            {
                var createdApplication = await _applicationsService.CreateApplicationAsync(application);
                return CreatedAtAction(nameof(GetApplication), new { id = createdApplication.ApplicationID }, createdApplication);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ApplicationsController)}.{nameof(CreateApplication)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the application.");
            }
        }

        /// <summary>
        /// Update an existing application.
        /// </summary>
        /// <param name="id">The ID of the application to update.</param>
        /// <param name="application">The updated application object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, ApplicationsModel application)
        {
            try
            {
                if (id != application.ApplicationID)
                {
                    return BadRequest();
                }

                await _applicationsService.UpdateApplicationAsync(id, application);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ApplicationsController)}.{nameof(UpdateApplication)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the application.");
            }
        }

        /// <summary>
        /// Delete an application by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the application to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                await _applicationsService.DeleteApplicationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ApplicationsController)}.{nameof(DeleteApplication)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the application.");
            }
        }
    }
}
