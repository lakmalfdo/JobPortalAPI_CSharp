using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class UsersService
    {
        private readonly List<UsersModel> _users = new List<UsersModel>();
        private int _nextUserId = 1;

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        public IEnumerable<UsersModel> GetUsers()
        {
            return _users;
        }

        /// <summary>
        /// Get a user by their unique ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        public UsersModel GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.UserID == userId);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        public UsersModel CreateUser(UsersModel user)
        {
            user.UserID = _nextUserId++;
            _users.Add(user);
            return user;
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        public void UpdateUser(int userId, UsersModel user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserID == userId);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.UserRole = user.UserRole;
                existingUser.ProfilePicture = user.ProfilePicture;
                existingUser.AuthenticationToken = user.AuthenticationToken;
            }
        }

        /// <summary>
        /// Delete a user by their unique ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUser(int userId)
        {
            var userToRemove = _users.FirstOrDefault(u => u.UserID == userId);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }
    }
}
