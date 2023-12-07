using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.PurchasedDate).LessThan(DateTime.Now);
            RuleFor(command => command.Model.PurchasedPrice).NotEmpty().GreaterThan(0).LessThan(99);
            RuleFor(command => command.Model.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
