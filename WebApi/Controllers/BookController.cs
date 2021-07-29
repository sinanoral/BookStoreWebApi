using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookDetailQuery;
using static WebApi.GetBooks.GetBooksQuery.GetBooksQuery;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]")]
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
        public List<BookViewModel> GetBooks()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(_context, _mapper);
            return booksQuery.Handle();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);

            BookDetailViewModel result;
            try
            {
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                getBookDetailQuery.Id = id;
                validator.ValidateAndThrow(getBookDetailQuery);
                result = getBookDetailQuery.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
            try
            {
                createBookCommand.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(createBookCommand);
                createBookCommand.Handle();

                //if (!result.IsValid)
                //    foreach (var it in result.Errors)
                //        Console.WriteLine("Property " + it.PropertyName + " Error Message: " + it.ErrorMessage);
                //else 
                //    createBookCommand.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            try
            {
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                updateBookCommand.Model = updatedBook;
                updateBookCommand.Id = id;
                validator.ValidateAndThrow(updateBookCommand);
                updateBookCommand.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Updated :)");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context); 
            try
            {
                deleteBookCommand.Id = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(deleteBookCommand);
                deleteBookCommand.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
