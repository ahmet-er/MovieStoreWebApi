using FluentValidation;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidator :AbstractValidator<GetCustomerDetailQuery>
    {
        public GetCustomerDetailQueryValidator()
        {
            RuleFor(command => command.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
