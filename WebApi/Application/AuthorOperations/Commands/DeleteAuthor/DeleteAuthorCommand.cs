using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int Id { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == Id);
            if (author is null)
                throw new InvalidOperationException("There is no author which has that id");

            if (_context.Books.Any(book => book.AuthorId == Id))
                throw new InvalidOperationException("Author can not be deleted because has published book");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

    }
}
