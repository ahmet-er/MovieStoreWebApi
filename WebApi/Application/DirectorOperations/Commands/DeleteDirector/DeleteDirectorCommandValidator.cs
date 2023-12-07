using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(x => x.DirectorId).NotEmpty().GreaterThan(0);
        }
    }
}
