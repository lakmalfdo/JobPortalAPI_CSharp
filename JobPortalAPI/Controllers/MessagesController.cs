using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Models;
using JobPortalAPI.Services;

namespace JobPortalAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService _messagesService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(MessagesService messagesService, ILogger<MessagesController> logger)
        {
            _messagesService = messagesService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all messages.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessagesModel>>>GetMessages()
        {
            try
            {
                var messages = await _messagesService.GetMessagesAsync();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessagesController)}.{nameof(GetMessages)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching messages.");
            }
        }

        /// <summary>
        /// Get a message by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the message to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<MessagesModel>> GetMessage(int id)
        {
            try
            {
                var message = await _messagesService.GetMessageAsync(id);
                if (message == null)
                {
                    return NotFound();
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessagesController)}.{nameof(GetMessage)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while fetching the message.");
            }
        }

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="message">The message object to create.</param>
        [HttpPost]
        public async Task<ActionResult<MessagesModel>> CreateMessage(MessagesModel message)
        {
            try
            {
                message.Timestamp = DateTime.Now; // Set the current timestamp.
                var createdMessage = await _messagesService.CreateMessageAsync(message);
                return CreatedAtAction(nameof(GetMessage), new { id = createdMessage.MessageID }, createdMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessagesController)}.{nameof(CreateMessage)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while creating the message.");
            }
        }

        /// <summary>
        /// Delete a message by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the message to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messagesService.DeleteMessageAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessagesController)}.{nameof(DeleteMessage)} - An error occurred: {ex}");
                return StatusCode(500, "An error occurred while deleting the message.");
            }
        }
    }
}
