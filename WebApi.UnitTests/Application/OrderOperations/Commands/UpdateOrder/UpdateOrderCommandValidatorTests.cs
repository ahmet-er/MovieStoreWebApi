using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(-1, -1, -1)]
        [InlineData(-1, 1, 100)]
        [InlineData(1, -1, 100)]
        [InlineData(1, 1, -10)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, int movieId, double price)
        {
            // arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null, null);
            command.OrderId = 1;

            command.Model = new UpdateOrderModel
            {
                CustomerId = customerId,
                MovieId = movieId,
                PurchasedPrice = price,
                PurchasedDate = DateTime.Now.Date.AddDays(-10)
            };

            // act 
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null, null);
            command.Model = new UpdateOrderModel
            {
                CustomerId = 30,
                MovieId = 30,
                PurchasedPrice = 30,
                PurchasedDate = DateTime.Now.Date
            };

            // act
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null, null);
            command.OrderId = 1;
            command.Model = new UpdateOrderModel
            {
                CustomerId = 20,
                MovieId = 20,
                PurchasedPrice = 50,
                PurchasedDate = DateTime.Now.Date.AddDays(-20)
            };

            // act
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
