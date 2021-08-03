using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.UnitTests.TestsSetup;
using Xunit;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord OF the", 0, 0, 0)]
        [InlineData("Lord OF the", 0, 1, 2)]
        [InlineData("Lor", 0, 0, 2)]
        [InlineData("", -1, 110, 22)]
        [InlineData(" ", 100, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel
            {
                Title = title,
                PageCount = pageCount,
                AuthorId = authorId,
                GenreId = genreId,
                PublishDate = DateTime.Now.Date.AddDays(-3)
            };

            CreateBookCommandValidator validator = new();
            validator.Validate(command).Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
