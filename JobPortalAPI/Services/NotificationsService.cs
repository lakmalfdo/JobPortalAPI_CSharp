using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class NotificationsService
    {
        private readonly List<NotificationsModel> _notifications = new List<NotificationsModel>();
        private int _nextNotificationID = 1;

        /// <summary>
        /// Get a list of all notifications.
        /// </summary>
        public IEnumerable<NotificationsModel> GetNotifications()
        {
            return _notifications;
        }

        /// <summary>
        /// Get a notification by its unique ID.
        /// </summary>
        /// <param name="notificationID">The ID of the notification to retrieve.</param>
        public NotificationsModel GetNotification(int notificationID)
        {
            return _notifications.FirstOrDefault(n => n.NotificationID == notificationID);
        }

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="notification">The notification object to create.</param>
        public NotificationsModel CreateNotification(NotificationsModel notification)
        {
            notification.NotificationID = _nextNotificationID++;
            notification.Timestamp = DateTime.Now; // Set the current timestamp.
            _notifications.Add(notification);
            return notification;
        }

        /// <summary>
        /// Delete a notification by its unique ID.
        /// </summary>
        /// <param name="notificationID">The ID of the notification to delete.</param>
        public void DeleteNotification(int notificationID)
        {
            var notificationToRemove = _notifications.FirstOrDefault(n => n.NotificationID == notificationID);
            if (notificationToRemove != null)
            {
                _notifications.Remove(notificationToRemove);
            }
        }
    }
}
