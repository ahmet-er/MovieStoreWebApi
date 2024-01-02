using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int authorId)
        {
            //arrange
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.ActorId = authorId;

            //act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.ActorId = 1;

            //act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
