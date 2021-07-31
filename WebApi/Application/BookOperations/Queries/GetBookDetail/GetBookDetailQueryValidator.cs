using FluentValidation;
using WebApi.BookOperations.GetBooks;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.Id).NotNull().GreaterThan(0);
        }
    }
}
