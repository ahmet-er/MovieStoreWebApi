using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("ahmet", "er", "aaaa.com", "password")]
        [InlineData("ae", "er", "aa@aa.com", "password")]
        [InlineData("ahmet", "e", "aaa.com", "password")]
        [InlineData("ahmet", "er", "aa@aa.com", "1234")]
        [InlineData("ahmet", "e", "aa@aa.com", "123456")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName, string email, string password)
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            // act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                FirstName = "true",
                LastName = "detective",
                Email = "x@x.com",
                Password= "password"
            };

            // act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
