using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.GetBooks.GetBooksQuery
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BookViewModel> Handle()
        {
            var books = _dbContext.Books.ToList();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in books)
            {
                vm.Add(new BookViewModel
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    Genre = ((GenreEnum)book.GenreId).ToString()
                });
            }

            return vm;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
