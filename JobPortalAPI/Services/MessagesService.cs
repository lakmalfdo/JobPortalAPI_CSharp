using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    public class MessagesService
    {
        private readonly List<MessagesModel> _messages = new List<MessagesModel>();
        private int _nextMessageID = 1;

        /// <summary>
        /// Get a list of all messages.
        /// </summary>
        public IEnumerable<MessagesModel> GetMessages()
        {
            return _messages;
        }

        /// <summary>
        /// Get a message by its unique ID.
        /// </summary>
        /// <param name="messageID">The ID of the message to retrieve.</param>
        public MessagesModel GetMessage(int messageID)
        {
            return _messages.FirstOrDefault(m => m.MessageID == messageID);
        }

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="message">The message object to create.</param>
        public MessagesModel CreateMessage(MessagesModel message)
        {
            message.MessageID = _nextMessageID++;
            _messages.Add(message);
            return message;
        }

        /// <summary>
        /// Delete a message by its unique ID.
        /// </summary>
        /// <param name="messageID">The ID of the message to delete.</param>
        public void DeleteMessage(int messageID)
        {
            var messageToRemove = _messages.FirstOrDefault(m => m.MessageID == messageID);
            if (messageToRemove != null)
            {
                _messages.Remove(messageToRemove);
            }
        }
    }
}
