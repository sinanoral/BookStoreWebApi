using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("There is no book to update which has that id");

            book.GenreId = Model.GenreId == default ? book.GenreId : Model.GenreId;
            book.Title = Model.Title == default ? book.Title : Model.Title;

            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
