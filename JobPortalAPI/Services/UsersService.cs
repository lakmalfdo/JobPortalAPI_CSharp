using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing users using Entity Framework and an injected DbContext.
    /// </summary>
    public class UsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all users asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of users.</returns>
        public async Task<IEnumerable<UsersModel>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Get a user by their unique ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>An asynchronous operation that returns the user with the specified ID.</returns>
        public async Task<UsersModel?> GetUserAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
        }

        /// <summary>
        /// Create a new user asynchronously.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created user.</returns>
        public async Task<UsersModel> CreateUserAsync(UsersModel user)
        {
            user.UserID = 0; // EF Core will auto-generate the ID
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Update an existing user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>An asynchronous operation to update the user.</returns>
        public async Task UpdateUserAsync(int userId, UsersModel user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.UserRole = user.UserRole;
                existingUser.ProfilePicture = user.ProfilePicture;
                existingUser.AuthenticationToken = user.AuthenticationToken;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a user by their unique ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An asynchronous operation to delete the user.</returns>
        public async Task DeleteUserAsync(int userId)
        {
            var userToRemove = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (userToRemove != null)
            {
                _context.Users.Remove(userToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
