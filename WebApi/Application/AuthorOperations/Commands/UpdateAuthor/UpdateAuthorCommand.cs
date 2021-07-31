using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model { get; set; }
        public int Id { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == Id);
            if (author is null)
                throw new InvalidOperationException("There is no author which has that id");

            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.Birthdate = Model.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }

}
