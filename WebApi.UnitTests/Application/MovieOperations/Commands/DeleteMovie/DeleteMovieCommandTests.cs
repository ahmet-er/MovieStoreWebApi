using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotMovieInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(dbContext);
            command.MovieId = 99;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot found the movie.");
        }

        [Fact]
        public void WhenIfMovieHaveAnyActor_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(dbContext);
            command.MovieId = 1;

            // act & assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot delete beacuse movie have actors.");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeDeleted()
        {
            // arrange
            var movie = new Movie()
            {
                DirectorId = 2,
                GenreId = 1,
                Name = "Delete Movie 3",
                Price = 49,
                PublishDate = DateTime.Now.Date.AddYears(-4)
            };
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            DeleteMovieCommand command = new DeleteMovieCommand(dbContext);
            command.MovieId = movie.Id;

            // act
            command.Handle();

            // assert
            var deletedMovie = dbContext.Movies.Find(movie.Id);
            deletedMovie.Should().BeNull();
        }
    }
}
