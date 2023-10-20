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

        public EmployersController(EmployersService employersService)
        {
            _employersService = employersService;
        }

        /// <summary>
        /// Get a list of all employers.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<EmployersModel> >GetEmployers()
        {
            return Ok(_employersService.GetEmployers());
        }

        /// <summary>
        /// Get an employer by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<EmployersModel> GetEmployer(int id)
        {
            var employer = _employersService.GetEmployer(id);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }

        /// <summary>
        /// Create a new employer.
        /// </summary>
        /// <param name="employer">The employer object to create.</param>
        [HttpPost]
        public ActionResult<EmployersModel> CreateEmployer(EmployersModel employer)
        {
            var createdEmployer = _employersService.CreateEmployer(employer);
            return CreatedAtAction(nameof(GetEmployer), new { id = createdEmployer.EmployerID }, createdEmployer);
        }

        /// <summary>
        /// Update an existing employer.
        /// </summary>
        /// <param name="id">The ID of the employer to update.</param>
        /// <param name="employer">The updated employer object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateEmployer(int id, EmployersModel employer)
        {
            if (id != employer.EmployerID)
            {
                return BadRequest();
            }

            _employersService.UpdateEmployer(id, employer);

            return NoContent();
        }

        /// <summary>
        /// Delete an employer by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employer to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployer(int id)
        {
            _employersService.DeleteEmployer(id);
            return NoContent();
        }
    }
}
