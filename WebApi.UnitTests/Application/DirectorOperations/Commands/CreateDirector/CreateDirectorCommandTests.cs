using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistDirectorFirstNameAndLastNameAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var director = new Director
            {
                FirstName = "Create",
                LastName = "Director",
            };
            dbContext.Directors.Add(director);
            dbContext.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(dbContext, mapper);
            command.Model = new CreateDirectorModel()
            {
                FirstName = director.FirstName,
                LastName = director.LastName
            };

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("The director has already in db.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            // arrange
            CreateDirectorCommand command = new CreateDirectorCommand(dbContext, mapper);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                FirstName = "New",
                LastName = "Director"
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var director = dbContext.Directors.SingleOrDefault(director => director.FirstName == model.FirstName && director.LastName == model.LastName);
            director.Should().NotBeNull();
            director.FirstName.Should().Be(model.FirstName);
            director.LastName.Should().Be(model.LastName);
        }
    }
}
