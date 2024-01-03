using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("ahmet", "er", "aaaa.com", "password")]
        [InlineData("ae", "er", "aa@aa.com", "password")]
        [InlineData("ahmet", "e", "aaa.com", "password")]
        [InlineData("ahmet", "er", "aa@aa.com", "1234")]
        [InlineData("ahmet", "e", "aa@aa.com", "123456")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName, string email, string password)
        {
            // arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null, null);
            command.CustomerId = 1;

            command.Model = new UpdateCustomerModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password

            };

            // act 
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null, null);
            command.CustomerId = 1;
            command.Model = new UpdateCustomerModel
            {
                FirstName = "updated",
                LastName = "customer",
                Email = "x@x.com",
                Password = "password edited"
            };

            // act
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
