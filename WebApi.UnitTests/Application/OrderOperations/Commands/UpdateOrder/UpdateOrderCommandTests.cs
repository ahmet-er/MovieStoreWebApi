using AutoMapper;
using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateOrderCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotOrderInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            UpdateOrderCommand command = new UpdateOrderCommand(dbContext, mapper);
            command.OrderId = 999;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("There is no order in db");
        }
        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated()
        {
            // arrange
            var initialOrder = new Order
            {
                CustomerId = 20,
                MovieId = 20,
                PurchasedPrice = 50,
                PurchasedDate = DateTime.Now.Date.AddDays(-20)
            };
            dbContext.Orders.Add(initialOrder);
            dbContext.SaveChanges();

            UpdateOrderCommand command = new UpdateOrderCommand(dbContext, mapper);
            command.OrderId = initialOrder.Id;

            var updatedOrderModel = new UpdateOrderModel
            {
                CustomerId = 20,
                MovieId = 10,
                PurchasedPrice = 75,
                PurchasedDate = DateTime.Now.Date.AddDays(-20)
            };
            command.Model = updatedOrderModel;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
