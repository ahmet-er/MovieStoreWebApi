using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistActorFirstNameAndLastNameAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var actor = new Actor
            {
                FirstName = "Test",
                LastName = "Actor",
            };
            dbContext.Actors.Add(actor);
            dbContext.SaveChanges();

            CreateActorCommand command = new CreateActorCommand(dbContext, mapper);
            command.Model = new CreateActorModel()
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName
            };

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("This actor is already registered in the database.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(dbContext, mapper);
            CreateActorModel model = new CreateActorModel()
            {
                FirstName = "New",
                LastName = "Actor"
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var actor = dbContext.Actors.SingleOrDefault(actor => actor.FirstName == model.FirstName && actor.LastName == model.LastName);
            actor.Should().NotBeNull();
            actor.FirstName.Should().Be(model.FirstName);
            actor.LastName.Should().Be(model.LastName);
        }
    }
}
