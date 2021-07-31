using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int Id { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == Id);
            if (genre is null)
                throw new InvalidOperationException("There is no genre which has that id");

            if (_context.Genres.Any(genre => genre.Name.ToLower() == Model.Name.ToLower() && genre.Id != Id))
                throw new InvalidOperationException("There is already a genre which has that name");

            genre.IsActive = Model.IsActive;
            genre.Name = Model.Name.Trim() == default ? genre.Name : Model.Name;
            _context.SaveChanges();
        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
