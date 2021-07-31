using FluentValidation;
using System;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
