using FluentAssertions;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int orderId)
        {
            //arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = orderId;

            //act
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = 1;

            //act
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
