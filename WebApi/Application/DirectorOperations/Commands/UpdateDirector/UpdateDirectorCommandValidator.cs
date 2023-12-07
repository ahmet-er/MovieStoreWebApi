using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.DirectorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2);
        }
    }
}
