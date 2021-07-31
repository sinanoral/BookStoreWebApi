using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(4);
            RuleFor(command => command.Model.GenreId).NotNull().GreaterThan(0);
            RuleFor(command => command.Id).NotNull().GreaterThan(0);
        }
    }
}
