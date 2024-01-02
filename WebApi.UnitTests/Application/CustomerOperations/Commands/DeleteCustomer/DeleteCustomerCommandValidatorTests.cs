using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int actorId)
        {
            // arrange 
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = actorId;

            // act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = 1;

            // act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
