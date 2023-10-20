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

        public ApplicationsController(ApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        /// <summary>
        /// Get a list of all applications.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationsModel>> GetApplications()
        {
            return Ok(_applicationsService.GetApplications());
        }

        /// <summary>
        /// Get an application by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the application to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<ApplicationsModel> GetApplication(int id)
        {
            var application = _applicationsService.GetApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        /// <summary>
        /// Create a new application.
        /// </summary>
        /// <param name="application">The application object to create.</param>
        [HttpPost]
        public ActionResult<ApplicationsModel> CreateApplication(ApplicationsModel application)
        {
            var createdApplication = _applicationsService.CreateApplication(application);
            return CreatedAtAction(nameof(GetApplication), new { id = createdApplication.ApplicationID }, createdApplication);
        }

        /// <summary>
        /// Update an existing application.
        /// </summary>
        /// <param name="id">The ID of the application to update.</param>
        /// <param name="application">The updated application object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateApplication(int id, ApplicationsModel application)
        {
            if (id != application.ApplicationID)
            {
                return BadRequest();
            }

            _applicationsService.UpdateApplication(id, application);

            return NoContent();
        }

        /// <summary>
        /// Delete an application by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the application to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            _applicationsService.DeleteApplication(id);
            return NoContent();
        }
    }
}
