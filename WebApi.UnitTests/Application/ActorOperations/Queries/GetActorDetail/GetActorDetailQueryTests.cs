using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenActorIdIsValid_Actor_ShouldNotBeReturnError()
        {
            // arrange
            var actor = new Actor { FirstName = "first name", LastName = "last name" };
            dbContext.Actors.Add(actor);
            dbContext.SaveChanges();

            GetActorDetailQuery query = new GetActorDetailQuery(dbContext, mapper);
            query.ActorId = actor.Id;

            // act
            var result = query.Handle();

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(actor.FirstName);
            result.LastName.Should().Be(actor.LastName);
        }
    }
}
