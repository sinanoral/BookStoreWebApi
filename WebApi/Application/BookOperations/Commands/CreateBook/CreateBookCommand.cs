using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookModel Model { get; set; }
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Book already included");

            book = _mapper.Map<Book>(Model); /*new Book();*/
            //book.Title = Model.Title;
            //book.GenreId = Model.GenreId;
            //book.PageCount = Model.PageCount;
            //book.PublishDate = Model.PublishDate;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }

        }
    }
}
