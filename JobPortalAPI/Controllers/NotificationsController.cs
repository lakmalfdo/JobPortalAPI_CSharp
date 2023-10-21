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
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(NotificationsService notificationsService, ILogger<NotificationsController> logger)
        {
            _notificationsService = notificationsService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all notifications.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationsModel>>>GetNotifications()
        {
            try
            {
                var notifications = await _notificationsService.GetNotificationsAsync();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(NotificationsController)}.{nameof(GetNotifications)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching notifications.");
            }
        }

        /// <summary>
        /// Get a notification by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the notification to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationsModel>> GetNotification(int id)
        {
            try
            {
                var notification = await _notificationsService.GetNotificationAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(NotificationsController)}.{nameof(GetNotification)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the notification.");
            }
        }

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="notification">The notification object to create.</param>
        [HttpPost]
        public async Task<ActionResult<NotificationsModel>> CreateNotification(NotificationsModel notification)
        {
            try
            {
                var createdNotification = await _notificationsService.CreateNotificationAsync(notification);
                return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.NotificationID }, createdNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(NotificationsController)}.{nameof(CreateNotification)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the notification.");
            }
        }

        /// <summary>
        /// Delete a notification by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the notification to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationsService.DeleteNotificationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(NotificationsController)}.{nameof(DeleteNotification)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the notification.");
            }
        }
    }
}
