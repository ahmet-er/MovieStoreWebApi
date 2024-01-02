using AutoMapper;
using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistorderIdAndCustomerIdAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var order = new Order
            {
                CustomerId = 1,
                MovieId = 2,
                PurchasedDate = DateTime.Now.Date.AddDays(-5),
                PurchasedPrice = 10,
                IsDeleted = false
            };
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            CreateOrderCommand command = new CreateOrderCommand(dbContext, mapper);
            command.Model = new CreateOrderModel()
            {
                CustomerId = order.CustomerId,
                MovieId = order.MovieId,
                PurchasedDate = order.PurchasedDate,
                PurchasedPrice = order.PurchasedPrice
            };

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("You buyed this movie before.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
        {
            // arrange
            CreateOrderCommand command = new CreateOrderCommand(dbContext, mapper);
            CreateOrderModel model = new CreateOrderModel()
            {
                CustomerId = 1,
                MovieId = 2,
                PurchasedDate = DateTime.Now.Date.AddDays(-5),
                PurchasedPrice = 10,
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var order = dbContext.Orders.SingleOrDefault(order => order.CustomerId == model.CustomerId && order.MovieId == model.MovieId);
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(model.CustomerId);
            order.MovieId.Should().Be(model.MovieId);
            order.PurchasedPrice.Should().Be(model.PurchasedPrice);
            order.PurchasedDate.Should().Be(model.PurchasedDate);
        }
    }
}
