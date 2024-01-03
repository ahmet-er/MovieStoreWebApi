using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotDirectorInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            UpdateDirectorCommand command = new UpdateDirectorCommand(dbContext, mapper);
            command.DirectorId = 324;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Cannot found the director");
        }
        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated()
        {
            // arrange
            var initialDirector = new Director { FirstName = "initial", LastName = "director" };
            dbContext.Directors.Add(initialDirector);
            dbContext.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(dbContext, mapper);
            command.DirectorId = initialDirector.Id;

            var updatedDirectorModel = new UpdateDirectorModel { FirstName = "initial", LastName = "director edited" };
            command.Model = updatedDirectorModel;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
