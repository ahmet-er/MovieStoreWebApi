using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotDirectorInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(dbContext);
            command.DirectorId = 99;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot found the director.");
        }

        [Fact]
        public void WhenIfDirectorHaveAnyMovie_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(dbContext);
            command.DirectorId = 1;

            // act & assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("You can't delete this director beacuse this director has a movie.");
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeDeleted()
        {
            // arrange
            var director = new Director()
            {
                FirstName = "delete",
                LastName = "director"
            };
            dbContext.Directors.Add(director);
            dbContext.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(dbContext);
            command.DirectorId = director.Id;

            // act
            command.Handle();

            // assert
            var deletedDirector = dbContext.Directors.Find(director.Id);
            deletedDirector.Should().BeNull();
        }
    }
}
