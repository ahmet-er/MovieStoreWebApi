using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotActorInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(dbContext);
            command.ActorId = 99;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("No actor to delete was found.");
        }

        [Fact]
        public void WhenIfActorHaveAnyMovie_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(dbContext);
            command.ActorId = 1;

            // act & assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot delete this actor because he/she have any movie.");
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Actor_ShouldBeDeleted()
        {
            // arrange
            var actor = new Actor()
            {
                FirstName = "delete",
                LastName = "actor"
            };
            dbContext.Actors.Add(actor);
            dbContext.SaveChanges();

            DeleteActorCommand command = new DeleteActorCommand(dbContext);
            command.ActorId = actor.Id;

            // act
            command.Handle();

            // assert
            var deletedActor = dbContext.Actors.Find(actor.Id);
            deletedActor.Should().BeNull();
        }
    }
}
