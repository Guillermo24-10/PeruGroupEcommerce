using FluentValidation;
using PeruGroup.Ecommerce.Application.DTO;

namespace PeruGroup.Ecommerce.Application.Validator
{
    public class UsersDtoValidator : AbstractValidator<UsersDTO>
    {
        public UsersDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty();
        }
    }
}
