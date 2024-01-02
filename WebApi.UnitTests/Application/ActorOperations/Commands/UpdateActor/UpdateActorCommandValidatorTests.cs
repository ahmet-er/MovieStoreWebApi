using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "sadsada")]
        [InlineData(" ", "sadsada")]
        [InlineData("hbfdhgre", "")]
        [InlineData("hbfdhgre", " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName)
        {
            // arrange
            UpdateActorCommand command = new UpdateActorCommand(null, null);
            command.ActorId = 1;

            command.Model = new UpdateActorModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            // act 
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateActorCommand command = new UpdateActorCommand(null, null);
            command.ActorId = 1;
            command.Model = new UpdateActorModel
            {
                FirstName = "update",
                LastName = "actor edited"
            };

            // act
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
