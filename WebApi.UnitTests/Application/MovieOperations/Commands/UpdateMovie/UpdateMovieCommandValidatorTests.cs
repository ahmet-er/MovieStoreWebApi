using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, 0, 0, "")]
        [InlineData(-1, -1, -1, "")]
        [InlineData(-1, 1, 100, "Validate Movie Name")]
        [InlineData(1, -1, 100, "Validate Movie Name")]
        [InlineData(1, 1, -10, "Validate Movie Name")]
        [InlineData(1, 1, 100, "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int directorId, int genreId, double price, string movieName)
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null, null);
            command.MovieId = 1;

            command.Model = new UpdateMovieModel
            {
                DirectorId = directorId,
                GenreId = genreId,
                Name = movieName,
                PublishDate = DateTime.Now.Date.AddDays(-10),
                Price = price
            };

            // act 
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null, null);
            command.Model = new UpdateMovieModel
            {

                DirectorId = 7,
                GenreId = 7,
                Name = "Test DateTime",
                Price = 15,
                PublishDate = DateTime.Now.Date
            };

            // act
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null, null);
            command.MovieId = 1;
            command.Model = new UpdateMovieModel
            {
                DirectorId = 9,
                GenreId = 9,
                Name = "Test Valid Movie",
                Price = 78,
                PublishDate = DateTime.Now.Date.AddYears(-72)
            };

            // act
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
