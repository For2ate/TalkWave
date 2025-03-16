using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TalkWave.User.Api.Core.Interfaces;
using TalkWave.User.Models.UserModels.Request;

namespace TalkWave.User.Api.Controllers {

    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase {

        private readonly IValidator<UserRegisterRequestModel> _userRegisterValidator;
        private readonly IValidator<UserLoginRequestModel> _userLoginValidator;
        private readonly IAuthService _authService;

        public AuthController(IValidator<UserRegisterRequestModel> userRegisterValidator, IValidator<UserLoginRequestModel> userLoginValidator, IAuthService authService) {

            _authService = authService;
            _userRegisterValidator = userRegisterValidator;
            _userLoginValidator = userLoginValidator;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterRequestModel model) {

            try {

                ValidationResult validationResult = await _userRegisterValidator.ValidateAsync(model);

                if (!validationResult.IsValid) {

                    throw new Exception(validationResult.ToString());

                }

                var user = await _authService.RegisterAsync(model);

                return Ok(user);

            } catch (Exception ex) {

                return BadRequest(ex.Message);

            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginRequestModel model) {

            try {

                ValidationResult validationResult = await _userLoginValidator.ValidateAsync(model);

                if (!validationResult.IsValid) {

                    throw new Exception(validationResult.ToString());

                }

                var user = await _authService.LoginAsync(model);

                return Ok(user);


            } catch (Exception ex) {

                return BadRequest(ex.Message);

            }

        }

    }

}
