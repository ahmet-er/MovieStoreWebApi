using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdIsValid_Movie_ShouldNotBeReturnError()
        {
            // arrange
            var movie = new Movie
            {
                Name = "test movie",
                PublishDate = DateTime.Now.Date.AddYears(-30),
                GenreId = 1,
                DirectorId = 1,
                Price = 65
            };
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            GetMovieDetailQuery query = new GetMovieDetailQuery(dbContext, mapper);
            query.MovieId = movie.Id;

            // act
            var result = query.Handle();

            // assert
            result.Should().NotBeNull();
            result.Name.Should().Be(movie.Name);
            result.PublishDate.Should().Be(movie.PublishDate);
            result.GenreId.Should().Be(movie.GenreId);
            result.DirectorId.Should().Be(movie.DirectorId);
            result.Price.Should().Be(movie.Price);
        }
    }
}
