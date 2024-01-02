using AutoMapper;
using FluentAssertions;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetOrderDetailQueryTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenOrderIdIsValid_Order_ShouldNotBeReturnError()
        {
            // arrange
            var order = new Order
            {
                CustomerId = 1,
                MovieId = 1,
                PurchasedPrice = 53,
                PurchasedDate = DateTime.Now.Date.AddYears(-1),
                IsDeleted = false
            };
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            GetOrderDetailQuery query = new GetOrderDetailQuery(dbContext, mapper);
            query.OrderId = order.Id;

            // act
            var result = query.Handle();

            // assert
            result.Should().NotBeNull();
            result.CustomerId.Should().Be(order.CustomerId);
            result.MovieId.Should().Be(order.MovieId);
            result.PurchasedPrice.Should().Be(order.PurchasedPrice);
            result.PurchasedDate.Should().Be(order.PurchasedDate);
        }
    }
}
