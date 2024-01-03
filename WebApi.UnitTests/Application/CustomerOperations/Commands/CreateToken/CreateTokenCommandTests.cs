using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public CreateTokenCommandTests(CommonTestFixture commonTestFixture)
        {
            context = commonTestFixture.Context;
            mapper = commonTestFixture.Mapper;
            configuration = commonTestFixture.Configuration;
        }
        [Fact]
        public void WhenInvalidCredentialsAreGiven_Handle_ShouldThrowInvalidOperationException()
        {
            // arrange
            var createTokenCommand = new CreateTokenCommand(context, mapper, configuration)
            {
                Model = new CreateTokenModel
                {
                    Email = "invalid@email.com",
                    Password = "invalidpassword"
                }
            };

            // act & assert
            createTokenCommand
                .Invoking(c => c.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Ooops.. Wrong Mail - Password.");
        }
            [Fact]
        public void WhenValidCredentialsAreGiven_Handle_ShouldReturnToken()
        {
            // arrange
            var initialCustomer = new Customer
            {
                FirstName = "Valid",
                LastName = "Customer",
                Email = "valid@email.com",
                Password = "validpassword"
            };
            context.Customers.Add(initialCustomer);
            context.SaveChanges();

            var createTokenCommand = new CreateTokenCommand(context, mapper, configuration)
            {
                Model = new CreateTokenModel
                {
                    Email = "valid@email.com",
                    Password = "validpassword"
                }
            };

            // act
            var token = createTokenCommand.Handle();

            // assert
            token.Should().NotBeNull();
            token.RefreshToken.Should().NotBeNullOrEmpty();
            token.Expiration.Should().BeAfter(DateTime.Now);
        }
    }
}
