using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistCustomerFirstNameAndLastNameAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var customer = new Customer
            {
                FirstName = "Create",
                LastName = "Customer",
                Email = "a@a.com",
                Password = "password"
            };
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            CreateCustomerCommand command = new CreateCustomerCommand(dbContext, mapper);
            command.Model = new CreateCustomerModel()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password
            };

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("This customer have already exist.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(dbContext, mapper);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                FirstName = "New",
                LastName = "Customer",
                Email = "a@new.com",
                Password = "newpassword"
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var customer = dbContext.Customers.SingleOrDefault(x => x.Email == model.Email);
            customer.Should().NotBeNull();
            customer.FirstName.Should().Be(model.FirstName);
            customer.LastName.Should().Be(model.LastName);
            customer.Email.Should().Be(model.Email);
            customer.Password.Should().Be(model.Password);
        }
    }
}
