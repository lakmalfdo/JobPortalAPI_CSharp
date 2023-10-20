using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<UsersModel>> GetUsers()
        {
            return Ok(_usersService.GetUsers());
        }

        /// <summary>
        /// Get a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<UsersModel> GetUser(int id)
        {
            var user = _usersService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        [HttpPost]
        public ActionResult<UsersModel> CreateUser(UsersModel user)
        {
            var createdUser = _usersService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserID }, createdUser);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UsersModel user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _usersService.UpdateUser(id, user);

            return NoContent();
        }

        /// <summary>
        /// Delete a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _usersService.DeleteUser(id);
            return NoContent();
        }
    }
}
