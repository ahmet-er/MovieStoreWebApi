using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.SoftDeleteOrder
{
    public class DeleteOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int orderId)
        {
            // arrange 
            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(null, null);
            command.OrderId = orderId;
            command.Model = new SoftDeleteOrderModel { IsDeleted = false };
            // act
            SoftDeleteOrderCommandValdiator validator = new SoftDeleteOrderCommandValdiator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(null, null);
            command.OrderId = 1;
            command.Model = new SoftDeleteOrderModel { IsDeleted = false };

            // act
            SoftDeleteOrderCommandValdiator validator = new SoftDeleteOrderCommandValdiator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
