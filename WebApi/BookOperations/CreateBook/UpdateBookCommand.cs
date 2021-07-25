using System;
using System.Linq;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.BookOperations.CreateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }
        public int Id { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("There is no book which has that id");

            book.GenreId = Model.GenreId != default ? book.GenreId : Model.GenreId;
            book.PageCount = Model.PageCount != default ? book.PageCount : Model.PageCount;
            book.Title = Model.Title != default ? book.Title : Model.Title;
            book.PublishDate = Model.PublishDate != default ? book.PublishDate : Model.PublishDate;

            _dbContext.SaveChanges();
        }
    }
}
