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

        [HttpGet("Message/{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(Guid id) {

            try {

                var message = await _messageService.GetMessageById(id);

                return Ok(message);
                
            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);

            }

        }

        [HttpGet("Messages")]
        public async Task<IActionResult> GetNMessagesFromMessageAsync([FromQuery] GetNMessagesRequestModel model) {

            try {

                var messages = await _messageService.GetNMessagesFromMessageAsync(model);

                return Ok(messages);

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);

            }


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

        [HttpPut("Message")]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageRequestModel model) {

            try {

                var result = await _messageService.UpdateMessageAsync(model);

                return Ok(result);

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);

            }


        }

        [HttpDelete("Message/{id}")]
        public async Task<IActionResult> DeleteMessageByIdAsync(Guid id) {

            try {

                await _messageService.DeleteMessageByIdAsync(id);
                
                return Ok($"The messsage {id} is deleted.");

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);

            }


        }

    }

}
