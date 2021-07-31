using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteBAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteBAuthorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
