using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var movie = new Movie
            {
                Name = "Movie Name",
                DirectorId = 1,
                GenreId = 1,
                Price = 45,
                PublishDate = DateTime.Now.Date.AddYears(-60)
            };
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(dbContext, mapper);
            command.Model = new CreateMovieModel()
            {
                Name = movie.Name,
                DirectorId = movie.DirectorId,
                GenreId = movie.GenreId,
                Price = movie.Price,
                PublishDate = movie.PublishDate
            };

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("This movie already have db.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            // arrange
            CreateMovieCommand command = new CreateMovieCommand(dbContext, mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "New Movie",
                DirectorId = 1,
                GenreId = 1,
                Price = 55,
                PublishDate = DateTime.Now.Date.AddYears(-30)
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var movie = dbContext.Movies.SingleOrDefault(movie => movie.Name.ToLower() == model.Name.ToLower());
            movie.Should().NotBeNull();
            movie.Name.Should().Be(model.Name);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.GenreId.Should().Be(model.GenreId);
            movie.Price.Should().Be(model.Price);
            movie.PublishDate.Should().Be(model.PublishDate);
        }
    }
}
