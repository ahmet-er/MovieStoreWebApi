using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotCustomerInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(dbContext);
            command.CustomerId = 99;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("No customer to delete was found.");
        }

        [Fact]
        public void WhenIfCustomerHaveAnyOrder_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(dbContext);
            command.CustomerId = 1;

            // act & assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("You cannot delete this customer, because this customer have any order(s).");
        }

        [Fact]
        public void WhenValidCustomerIdIsGiven_Customer_ShouldBeDeleted()
        {
            // arrange
            var customer = new Customer()
            {
                FirstName = "delete",
                LastName = "customer",
                Email = "aa@a.com",
                Password = "password"
            };
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(dbContext);
            command.CustomerId = customer.Id;

            // act
            command.Handle();

            // assert
            var deletedActor = dbContext.Customers.Find(customer.Id);
            deletedActor.Should().BeNull();
        }
    }
}
