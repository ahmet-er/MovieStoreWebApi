using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("dasdasd", "")]
        [InlineData("", "fdsgfrwg")]
        [InlineData("dw", "wqeqwe")]
        [InlineData("dsad", "s")]
        [InlineData("sd", "g")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName)
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            // act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorModel()
            {
                FirstName = "true",
                LastName = "detective"
            };

            // act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
