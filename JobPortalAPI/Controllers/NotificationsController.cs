using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationsService _notificationsService;

        public NotificationsController(NotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        /// <summary>
        /// Get a list of all notifications.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<NotificationsModel>> GetNotifications()
        {
            return Ok(_notificationsService.GetNotifications());
        }

        /// <summary>
        /// Get a notification by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the notification to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<NotificationsModel> GetNotification(int id)
        {
            var notification = _notificationsService.GetNotification(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="notification">The notification object to create.</param>
        [HttpPost]
        public ActionResult<NotificationsModel> CreateNotification(NotificationsModel notification)
        {
            var createdNotification = _notificationsService.CreateNotification(notification);
            return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.NotificationID }, createdNotification);
        }

        /// <summary>
        /// Delete a notification by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the notification to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            _notificationsService.DeleteNotification(id);
            return NoContent();
        }
    }
}
