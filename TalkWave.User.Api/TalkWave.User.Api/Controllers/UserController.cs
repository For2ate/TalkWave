using AutoMapper.Configuration.Annotations;
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

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetUserByIdAsync(Guid id) {

            try {

                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);

            } catch (Exception ex) {

                return BadRequest(ex.Message);

            }

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

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email) {

            try {

                var user = await _userService.GetUserByEmailAsync(email);

                return Ok(user);

            } catch(Exception ex) {

                return BadRequest(ex.Message);

            }

        }

    }

}
