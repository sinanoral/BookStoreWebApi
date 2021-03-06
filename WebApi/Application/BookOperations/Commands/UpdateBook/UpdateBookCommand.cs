using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("There is no book to update which has that id");

            book.GenreId = Model.GenreId == default ? book.GenreId : Model.GenreId;
            book.AuthorId = Model.AuthorId == default ? book.AuthorId : Model.AuthorId;
            book.Title = Model.Title == default ? book.Title : Model.Title;

            _dbContext.SaveChanges();
        }

    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
