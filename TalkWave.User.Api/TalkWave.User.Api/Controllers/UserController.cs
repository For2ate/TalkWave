using Microsoft.AspNetCore.Mvc;
using TalkWave.User.Api.Core.Interfaces;

namespace Taskly.User.Api.Controllers {

    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {

            _userService = userService;
        
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsersAsync() {

            try {

                var users = await _userService.GetAllUsersAsync();

                return Ok(users);

            } catch (Exception ex) {

                return BadRequest(ex.Message);

            }

        }

    }

}
