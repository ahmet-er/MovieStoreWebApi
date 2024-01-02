using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int directorId)
        {
            // arrange 
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = directorId;

            // act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 1;

            // act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
