using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotActorInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            UpdateActorCommand command = new UpdateActorCommand(dbContext, mapper);
            command.ActorId = 324;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("No actor to update was found.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated()
        {
            // arrange
            var initialActor = new Actor { FirstName = "update", LastName = "actor" };
            dbContext.Actors.Add(initialActor);
            dbContext.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(dbContext, mapper);
            command.ActorId = initialActor.Id;

            var updatedActorModel = new UpdateActorModel { FirstName = "update", LastName = "actor edited" };
            command.Model = updatedActorModel;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
