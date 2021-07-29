﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using WebApi.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.CreateBook.UpdateBookCommand;
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
            GetBookDetailQuery getBookByIdQuery = new GetBookDetailQuery(_context, _mapper);

            BookDetailViewModel result;
            try
            {
                getBookByIdQuery.Id = id;
                result = getBookByIdQuery.Handle();
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
                createBookCommand.Handle();
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
                updateBookCommand.Model = updatedBook;
                updateBookCommand.Id = id;
                updateBookCommand.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("It is updated :)");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context); 
            try
            {
                deleteBookCommand.Id = id;
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
