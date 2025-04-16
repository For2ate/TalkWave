using Microsoft.AspNetCore.Mvc;
using Serilog;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Models.Chats.Request;

namespace TalkWave.Chat.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ChatsController : ControllerBase {

    private readonly IChatService _chatService;
    private readonly ILogger<ChatsController> _logger;

    public ChatsController(
        IChatService chatService,
        ILogger<ChatsController> logger) {

        _chatService = chatService;
        _logger = logger;

        _logger.LogInformation("ChatsController initialized");
    }

    [HttpGet("Chats/{id}")]
    public async Task<IActionResult> GetChatsForUserAsync(Guid id) {

        _logger.LogInformation("Getting chats for user {UserId}", id);

        try {

            var timer = System.Diagnostics.Stopwatch.StartNew();
            var chats = await _chatService.GetChatsForUserAsync(id);

            _logger.LogInformation(
                "Successfully retrieved {ChatCount} chats for user {UserId} in {ElapsedMs}ms",
                chats?.Count(),
                id,
                timer.ElapsedMilliseconds);

            return Ok(chats);

        } catch (Exception ex) {

            _logger.LogError(
                ex,
                "Error getting chats for user {UserId}. Error: {ErrorMessage}",
                id,
                ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Chat/Personal")]
    public async Task<IActionResult> CreatePersonalChatAsync(CreatePersonalChatModel model) {

        _logger.LogInformation(
            "Creating personal chat between {User1} and {User2}",
            model.SenderUserId,
            model.RecipientUserId);

        try {
            var chat = await _chatService.CreatePersonalChatAsync(model);

            if (chat != null) {

                _logger.LogInformation(
                    "Successfully created chat {ChatId} between {User1} and {User2}",
                    chat.Id,
                    model.SenderUserId,
                    model.RecipientUserId);

            }

            return Ok(chat);

        } catch (Exception ex) {

            _logger.LogError(
                ex,
                "Error creating chat between {User1} and {User2}. Error: {ErrorMessage}",
                model.SenderUserId,
                model.RecipientUserId,
                ex.Message);

            return BadRequest(ex.Message);

        }
    }
}
