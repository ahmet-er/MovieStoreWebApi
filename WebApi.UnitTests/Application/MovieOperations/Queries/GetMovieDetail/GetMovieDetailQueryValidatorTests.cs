using FluentAssertions;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int movieId)
        {
            //arrange
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = movieId;

            //act
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = 1;

            //act
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
