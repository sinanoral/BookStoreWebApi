using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;
using static WebApi.GetBooks.GetBooksQuery.GetBooksQuery;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("There is no book which has that id");

            BookViewModel vm = new BookViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();

            return vm;
        }
    }
}
