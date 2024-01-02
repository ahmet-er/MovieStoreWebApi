using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDirectorIdIsValid_Director_ShouldNotBeReturnError()
        {
            // arrange
            var director = new Director
            {
                FirstName = "first name",
                LastName = "last name"
            };
            dbContext.Directors.Add(director);
            dbContext.SaveChanges();

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(dbContext, mapper);
            query.DirectorId = director.Id;

            // act
            var result = query.Handle();

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(director.FirstName);
            result.LastName.Should().Be(director.LastName);
        }
    }
}
