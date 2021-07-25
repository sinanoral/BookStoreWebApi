using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using WebApi.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.GetBooks.GetBooksQuery.GetBooksQuery;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<BookViewModel> GetBooks()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(_context);
            return booksQuery.Handle();
        }

        [HttpGet("{id}")]
        public BookViewModel GetById(int id)
        {
            GetBookByIdQuery getBookByIdQuery = new GetBookByIdQuery(_context);
            getBookByIdQuery.Id = id;
            return getBookByIdQuery.Handle();
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context);
            try
            {
                createBookCommand.Model = newBook;
                createBookCommand.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] CreateBookModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            updateBookCommand.Model = updatedBook;
            updateBookCommand.Id = id;
            updateBookCommand.Handle();
            return Ok("It is updated :)");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
