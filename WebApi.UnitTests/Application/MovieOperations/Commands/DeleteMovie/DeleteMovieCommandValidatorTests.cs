using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int movieId)
        {
            // arrange 
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = movieId;

            // act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = 1;

            // act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
