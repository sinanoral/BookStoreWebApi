using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();

            Context.Genres.AddRange(
                    new Genre { Name = "Personal Growth" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Romance" }
                    );

            Context.Books.AddRange(
                new Book { GenreId = 1, PageCount = 255, PublishDate = DateTime.Today, Title = "Namik Kemal", AuthorId = 1 },
                new Book { GenreId = 2, PageCount = 155, PublishDate = DateTime.Today, Title = "Sinan Kemal", AuthorId = 2 },
                new Book { GenreId = 3, PageCount = 355, PublishDate = DateTime.Today, Title = "Ahmet Kemal", AuthorId = 3 }
               );

            Context.SaveChanges();
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        }
    }
}
