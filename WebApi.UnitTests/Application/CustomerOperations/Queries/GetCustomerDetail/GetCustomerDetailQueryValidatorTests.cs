using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int customerId)
        {
            //arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = customerId;

            //act
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 1;

            //act
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
