using ChatApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _messageService.GetMessages();
            return Ok(messages);
        }
    }
}