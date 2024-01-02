using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var con = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (con.Genres.Any())
                    return;

                var genres = new List<Genre>
                {
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Action" },
                    new Genre { Name = "Fantasy" },
                    new Genre { Name = "Horror" },
                    new Genre { Name = "Romance" },
                    new Genre { Name = "Western" },
                    new Genre { Name = "Thriller" }
                };

                con.Genres.AddRange(genres);
                con.SaveChanges();
            }
        }
    }
}
