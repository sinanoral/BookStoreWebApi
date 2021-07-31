using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Authors.AddRange(
                    new Author { Name = "Sinan", Surname = "Oral", Birthdate = new DateTime(1999, 05, 19) },
                    new Author { Name = "Bugra", Surname = "Durukan", Birthdate = new DateTime(1999, 12, 23) },
                    new Author { Name = "Selim", Surname = "Yesilkaya", Birthdate = new DateTime(1994, 05, 17) }
                    );

                context.Genres.AddRange(
                    new Genre { Name = "Personal Growth" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Romance" }
                    );

                context.Books.AddRange(
                    new Book { GenreId = 1, PageCount = 255, PublishDate = DateTime.Today, Title = "Namik Kemal", AuthorId = 1 },
                    new Book { GenreId = 2, PageCount = 155, PublishDate = DateTime.Today, Title = "Sinan Kemal", AuthorId = 2 },
                    new Book { GenreId = 3, PageCount = 355, PublishDate = DateTime.Today, Title = "Ahmet Kemal", AuthorId = 3 }
                   );

                context.SaveChanges();
            }
        }
    }
}
