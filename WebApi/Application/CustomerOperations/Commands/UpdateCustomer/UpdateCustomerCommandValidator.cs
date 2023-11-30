using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator() 
        {
            RuleFor(command => command.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(5);
        }
    }
}
