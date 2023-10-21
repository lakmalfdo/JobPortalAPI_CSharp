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
        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>>GetUsers()
        {
            try
            {
                var users = await _usersService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UsersController)}.{nameof(GetUsers)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }

        /// <summary>
        /// Get a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersModel>> GetUser(int id)
        {
            try
            {
                var user = await _usersService.GetUserAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UsersController)}.{nameof(GetUser)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the user.");
            }
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        [HttpPost]
        public async Task<ActionResult<UsersModel>> CreateUser(UsersModel user)
        {
            try
            {
                var createdUser = await _usersService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserID }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UsersController)}.{nameof(CreateUser)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UsersModel user)
        {
            try
            {
                if (id != user.UserID)
                {
                    return BadRequest();
                }

                await _usersService.UpdateUserAsync(id, user);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UsersController)}.{nameof(UpdateUser)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        /// <summary>
        /// Delete a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _usersService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UsersController)}.{nameof(DeleteUser)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
