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

        public MessagesController(MessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        /// <summary>
        /// Get a list of all messages.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<MessagesModel>> GetMessages()
        {
            return Ok(_messagesService.GetMessages());
        }

        /// <summary>
        /// Get a message by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the message to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<MessagesModel> GetMessage(int id)
        {
            var message = _messagesService.GetMessage(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="message">The message object to create.</param>
        [HttpPost]
        public ActionResult<MessagesModel> CreateMessage(MessagesModel message)
        {
            message.Timestamp = DateTime.Now; // Set the current timestamp.
            var createdMessage = _messagesService.CreateMessage(message);
            return CreatedAtAction(nameof(GetMessage), new { id = createdMessage.MessageID }, createdMessage);
        }

        /// <summary>
        /// Delete a message by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the message to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            _messagesService.DeleteMessage(id);
            return NoContent();
        }
    }
}
