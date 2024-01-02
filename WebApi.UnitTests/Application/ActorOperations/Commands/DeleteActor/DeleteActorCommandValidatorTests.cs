using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int actorId)
        {
            // arrange 
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = actorId;

            // act
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;

            // act
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);   

            // assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
