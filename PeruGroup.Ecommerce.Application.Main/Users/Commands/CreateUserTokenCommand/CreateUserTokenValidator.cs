using FluentValidation;

namespace PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenCommand>
    {
        public CreateUserTokenValidator()
        {
            RuleFor(u => u.username).NotNull().NotEmpty();
            RuleFor(u => u.password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
