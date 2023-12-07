using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class SoftDeleteOrderCommandValdiator : AbstractValidator<SoftDeleteOrderCommand>
    {
        public SoftDeleteOrderCommandValdiator()
        {
            RuleFor(command => command.OrderId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.IsDeleted).NotEmpty();
        }
    }
}
