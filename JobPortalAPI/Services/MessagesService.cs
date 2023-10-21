using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Services
{
    /// <summary>
    /// Service for managing messages using Entity Framework and an injected DbContext.
    /// </summary>
    public class MessagesService
    {
        private readonly ApplicationDbContext _context;

        public MessagesService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all messages asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of messages.</returns>
        public async Task<IEnumerable<MessagesModel>> GetMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        /// <summary>
        /// Get a message by its unique ID asynchronously.
        /// </summary>
        /// <param name="messageID">The ID of the message to retrieve.</param>
        /// <returns>An asynchronous operation that returns the message with the specified ID.</returns>
        public async Task<MessagesModel?> GetMessageAsync(int messageID)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.MessageID == messageID);
        }

        /// <summary>
        /// Create a new message asynchronously.
        /// </summary>
        /// <param name="message">The message object to create.</param>
        /// <returns>An asynchronous operation that returns the newly created message.</returns>
        public async Task<MessagesModel> CreateMessageAsync(MessagesModel message)
        {
            message.MessageID = 0; // EF Core will auto-generate the ID
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        /// <summary>
        /// Delete a message by its unique ID asynchronously.
        /// </summary>
        /// <param name="messageID">The ID of the message to delete.</param>
        /// <returns>An asynchronous operation to delete the message.</returns>
        public async Task DeleteMessageAsync(int messageID)
        {
            var messageToRemove = await _context.Messages.FirstOrDefaultAsync(m => m.MessageID == messageID);
            if (messageToRemove != null)
            {
                _context.Messages.Remove(messageToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
