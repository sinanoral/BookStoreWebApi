using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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

                context.Books.AddRange(
                    new Book { GenreId = 1, PageCount = 255, PublishDate = DateTime.Today, Title = "Namik Kemal" },
                    new Book { GenreId = 2, PageCount = 155, PublishDate = DateTime.Today, Title = "Sinan Kemal" },
                    new Book { GenreId = 3, PageCount = 355, PublishDate = DateTime.Today, Title = "Ahmet Kemal" }
                   );

                context.SaveChanges();
            }
        }
    }
}
