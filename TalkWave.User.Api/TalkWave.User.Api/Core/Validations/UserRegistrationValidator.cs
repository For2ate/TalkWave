using FluentValidation;
using TalkWave.User.Models.UserModels.Request;

namespace TalkWave.User.Api.Core.Validations {

    public class UserRegistrationValidator : AbstractValidator<UserRegisterRequestModel> {

        public UserRegistrationValidator() {

            // Валидация для Login
            RuleFor(user => user.Login)
                .NotEmpty().WithMessage("Login is required.")
                .MinimumLength(6).WithMessage("Login must be at least 6 characters long.")
                .MaximumLength(50).WithMessage("Login must be less than 50 characters.");

            // Валидация для Password
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(10).WithMessage("Password must be at least 10 characters long.")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
                .Matches(@"[!@#$%^&*()_+=\[\]{};:'\""<>,./?`~|]+")
                .WithMessage("Password must contain at least one special character.");

            // Валидация для Email
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            // Валидация для FirstName
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must be less than 100 characters.");

            // Валидация для LastName
            RuleFor(user => user.LastName)
                .MaximumLength(100).WithMessage("Last name must be less than 100 characters.");

        }

    }

}
