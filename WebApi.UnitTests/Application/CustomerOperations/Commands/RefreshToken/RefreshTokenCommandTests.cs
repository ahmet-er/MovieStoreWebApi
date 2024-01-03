using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;
        public RefreshTokenCommandTests(CommonTestFixture commonTestFixture)
        {
            dbContext = commonTestFixture.Context;
            configuration = commonTestFixture.Configuration;
        }
        [Fact]
        public void WhenInvalidRefreshTokenIsGiven_Handle_ShouldThrowInvalidOperationException()
        {
            // arrange
            var customer = new Customer
            {
                FirstName = "Invalid",
                LastName = "Customer",
                Email = "invalid@customer.com",
                Password = "password",
                RefreshToken = "invalid_refresh_token",
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(-5)
            };

            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var refreshTokenCommand = new RefreshTokenCommand(dbContext, configuration)
            {
                RefreshToken = "invalid_refresh_token"
            };

            // act & assert
            refreshTokenCommand
                .Invoking(x => x.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Cannot found a valid refresh token");
        }
        [Fact]
        public void WhenValidRefreshTokenIsGiven_Handle_ShouldReturnToken()
        {
            // arrange
            var customer = new Customer
            {
                FirstName = "Valid",
                LastName = "Customer",
                Email = "valid@customer.com",
                Password = "password",
                RefreshToken = "valid_refresh_token",
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(10)
            };

            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var refreshTokenCommand = new RefreshTokenCommand(dbContext, configuration)
            {
                RefreshToken = "valid_refresh_token"
            };

            // act
            var token = refreshTokenCommand.Handle();

            // assert
            token.Should().NotBeNull();
            token.RefreshToken.Should().NotBeNullOrEmpty();
            token.Expiration.Should().BeAfter(DateTime.Now);
        }
    }
}
