using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public int Id { get; set; }
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre => genre.IsActive && genre.Id == Id);
            if (genre is null)
                throw new InvalidOperationException("There is no genre which has that id");

            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
