using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateCustomerCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotCustomerInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            UpdateCustomerCommand command = new UpdateCustomerCommand(dbContext, mapper);
            command.CustomerId = 324;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("No customer to update was found.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Customer_ShouldBeUpdated()
        {
            // arrange
            var initialCustomer = new Customer 
            { 
                FirstName = "update", 
                LastName = "customer",
                Email = "update@customer.com",
                Password = "password"
            };
            dbContext.Customers.Add(initialCustomer);
            dbContext.SaveChanges();

            UpdateCustomerCommand command = new UpdateCustomerCommand(dbContext, mapper);
            command.CustomerId = initialCustomer.Id;

            var updatedCustomerModel = new UpdateCustomerModel
            {
                FirstName = "update",
                LastName = "customer edited",
                Email = "update@customer.com",
                Password = "password"
            };
            command.Model = updatedCustomerModel;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
