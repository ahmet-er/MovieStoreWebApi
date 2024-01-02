using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class SoftDeleteOrderCommandValdiator : AbstractValidator<SoftDeleteOrderCommand>
    {
        public SoftDeleteOrderCommandValdiator()
        {
            RuleFor(command => command.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}
