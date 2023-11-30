using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator() 
        {
            RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
