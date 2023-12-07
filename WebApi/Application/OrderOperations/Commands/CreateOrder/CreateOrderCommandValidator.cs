using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.Model.PurchasedDate).LessThan(DateTime.Now);
            RuleFor(command => command.Model.PurchasedPrice).NotEmpty().GreaterThan(0).LessThan(99);
            RuleFor(command => command.Model.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
