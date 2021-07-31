using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.GetGenres;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;


namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery genresQuery = new GetGenresQuery(_context, _mapper);
            return Ok(genresQuery.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
            GenreDetailViewModel result;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            getGenreDetailQuery.Id = id;
            validator.ValidateAndThrow(getGenreDetailQuery);
            result = getGenreDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand createGenreCommand = new CreateGenreCommand(_context, _mapper);
            createGenreCommand.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(createGenreCommand);
            createGenreCommand.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(_context);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            updateGenreCommand.Model = updatedGenre;
            updateGenreCommand.Id = id;
            validator.ValidateAndThrow(updateGenreCommand);
            updateGenreCommand.Handle();

            return Ok("Updated :)");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
            deleteGenreCommand.Id = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(deleteGenreCommand);
            deleteGenreCommand.Handle();

            return Ok();
        }
    }
}
