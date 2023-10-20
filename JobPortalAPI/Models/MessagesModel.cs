using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Models
{
    public class MessagesModel
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
