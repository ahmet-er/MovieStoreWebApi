using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "director")]
        [InlineData(" ", "director")]
        [InlineData("create", "")]
        [InlineData("create", " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName)
        {
            // arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
            command.DirectorId = 1;

            command.Model = new UpdateDirectorModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            // act 
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel
            {
                FirstName = "update",
                LastName = "director validate"
            };

            // act
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
