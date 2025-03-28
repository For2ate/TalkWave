using Microsoft.AspNetCore.Mvc;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Models.Messages.Request;

namespace TalkWave.Chat.Api.Controllers {

    [ApiController]
    [Route("Api/[controller]")]
    public class MessageController : ControllerBase {

        private readonly IMessageService _messageService;

        public MessageController (IMessageService messageService) {

            _messageService = messageService;

        }

        [HttpPost("Message")]
        public async Task<IActionResult> CreateMessageAsync(CreateMessageRequestModel model) {

            try {

                var response = await _messageService.CreateMessageAsync(model);

                return Ok(response);

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);

            } 

        }

    }

}
