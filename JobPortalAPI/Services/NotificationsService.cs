using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing notifications using Entity Framework and an injected DbContext.
    /// </summary>
    public class NotificationsService
    {
        private readonly ApplicationDbContext _context;

        public NotificationsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all notifications asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of notifications.</returns>
        public async Task<IEnumerable<NotificationsModel>> GetNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        /// <summary>
        /// Get a notification by its unique ID asynchronously.
        /// </summary>
        /// <param name="notificationID">The ID of the notification to retrieve.</param>
        /// <returns>An asynchronous operation that returns the notification with the specified ID.</returns>
        public async Task<NotificationsModel?> GetNotificationAsync(int notificationID)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationID == notificationID);
        }

        /// <summary>
        /// Create a new notification asynchronously.
        /// </summary>
        /// <param name="notification">The notification object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created notification.</returns>
        public async Task<NotificationsModel> CreateNotificationAsync(NotificationsModel notification)
        {
            notification.NotificationID = 0; // EF Core will auto-generate the ID
            notification.Timestamp = DateTime.Now; // Set the current timestamp.
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        /// <summary>
        /// Delete a notification by its unique ID asynchronously.
        /// </summary>
        /// <param name="notificationID">The ID of the notification to delete.</param>
        /// <returns>An asynchronous operation to delete the notification.</returns>
        public async Task DeleteNotificationAsync(int notificationID)
        {
            var notificationToRemove = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationID == notificationID);
            if (notificationToRemove != null)
            {
                _context.Notifications.Remove(notificationToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
