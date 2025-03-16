using FluentValidation;
using TalkWave.User.Models.UserModels.Request;

namespace TalkWave.User.Api.Core.Validations {

    public class UserLoginValidator : AbstractValidator<UserLoginRequestModel> {

        public UserLoginValidator() {

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login is required.") 
                .MaximumLength(50).WithMessage("Login must be less than 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

        }

    }

}
