using FluentValidation;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x=>x.CustomerId).NotNull().NotEmpty().MaximumLength(5); 
        }
    }
}
