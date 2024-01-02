using AutoMapper;
using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.SoftDeleteOrder
{
    public class DeleteOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DeleteOrderCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyHaveNotOrderInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(dbContext, mapper);
            command.OrderId = 99;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("There is no order in db.");
        }

        [Fact]
        public void WhenValidOrderIdIsGiven_Order_ShouldBeDeleted()
        {
            // arrange
            var order = new Order()
            {
                CustomerId = 5,
                MovieId = 4,
                PurchasedDate = DateTime.Now.Date.AddYears(-4),
                PurchasedPrice = 67,
                IsDeleted = false
            };
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(dbContext, mapper);
            command.OrderId = order.Id;

            // act
            command.Handle();

            // assert
            var deletedOrder = dbContext.Orders.Find(order.Id);
            deletedOrder.IsDeleted.Should().Be(true);
        }
    }
}
