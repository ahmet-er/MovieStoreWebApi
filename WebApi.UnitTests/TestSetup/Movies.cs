using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this ApplicationDbContext context)
        {
            context.Movies.AddRange
                (
                    new Movie
                    {
                        Id = 1,
                        DirectorId = 1,
                        GenreId = 1,
                        Name = "Test 1",
                        Price = 10,
                        PublishDate = DateTime.Now.AddYears(-10)
                    },
                    new Movie
                    {
                        Id = 2,
                        DirectorId = 2,
                        GenreId = 2,
                        Name = "Test 2",
                        Price = 20,
                        PublishDate = DateTime.Now.AddYears(-20)
                    },
                    new Movie
                    {
                        Id = 3,
                        DirectorId = 3,
                        GenreId = 3,
                        Name = "Test 3",
                        Price = 30,
                        PublishDate = DateTime.Now.AddYears(-30)
                    }
                );
        }
    }
}
