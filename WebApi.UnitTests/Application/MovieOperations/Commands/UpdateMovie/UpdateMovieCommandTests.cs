using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotMovieInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            UpdateMovieCommand command = new UpdateMovieCommand(dbContext, mapper);
            command.MovieId = 324;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot find the movie.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated()
        {
            // arrange
            var initialMovie = new Movie
            {
                DirectorId = 6,
                GenreId = 3,
                Name = "Foo",
                Price = 63,
                PublishDate = DateTime.Now.Date.AddYears(-15)
            };
            dbContext.Movies.Add(initialMovie);
            dbContext.SaveChanges();

            UpdateMovieCommand command = new UpdateMovieCommand(dbContext, mapper);
            command.MovieId = initialMovie.Id;

            var updatedMovieModel = new UpdateMovieModel
            {
                DirectorId = 6,
                GenreId = 3,
                Name = "Foo edited",
                Price = 75,
                PublishDate = DateTime.Now.Date.AddYears(-15)
            };
            command.Model = updatedMovieModel;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
