using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class MovieActors
    {
        public static void AddMovieActors(this ApplicationDbContext context)
        {
            context.MovieActors.AddRange
                (
                    new MovieActor { ActorId = 1, MovieId = 1 },
                    new MovieActor { ActorId = 2, MovieId = 2 },
                    new MovieActor { ActorId = 3, MovieId = 3 }
                );
        }
    }
}
