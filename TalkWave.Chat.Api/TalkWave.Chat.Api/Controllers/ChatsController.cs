using Microsoft.AspNetCore.Mvc;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Api.Controllers {


    [ApiController]
    [Route("Api/[controller]")]
    public class ChatsController : ControllerBase {

        private readonly IChatService chatService;

        public ChatsController(IChatService chatService) {
            this.chatService = chatService;
        } 

        [HttpGet("Chats/{id}")]
        public async Task<IActionResult> GetChatsForUserAsync(Guid id) {

            try {

                var chats = await chatService.GetChatsForUserAsync(id);

                return Ok(chats);

            } catch( Exception ex) {

                Console.WriteLine("Controller error./n/n/n" + ex.Message);

                return BadRequest(ex.Message);

            }

        }


    }

}
