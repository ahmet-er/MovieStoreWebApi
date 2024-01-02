using FluentAssertions;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int directorId)
        {
            //arrange
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
            query.DirectorId = directorId;

            //act
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
            query.DirectorId = 1;

            //act
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
