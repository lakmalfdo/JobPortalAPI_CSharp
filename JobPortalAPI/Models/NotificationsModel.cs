using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class NotificationsModel
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }

        [Required]
        public string NotificationContent { get; set; }
        public string NotificationType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
