using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookDetailQuery;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(_context, _mapper);
            return Ok(booksQuery.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            BookDetailViewModel result;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            getBookDetailQuery.Id = id;
            validator.ValidateAndThrow(getBookDetailQuery);
            result = getBookDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
            createBookCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            updateBookCommand.Model = updatedBook;
            updateBookCommand.Id = id;
            validator.ValidateAndThrow(updateBookCommand);
            updateBookCommand.Handle();

            return Ok("Updated :)");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.Id = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();

            return Ok();
        }
    }
}
