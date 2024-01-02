using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetCustomerDetailQueryTests(CommonTestFixture testFixture)
        {
            dbContext = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenCustomerIdIsValid_Customer_ShouldNotBeReturnError()
        {
            // arrange
            var customer = new Customer 
            { 
                FirstName = "first name", 
                LastName = "last name",
                Email = "a@a.com",
                Password = "password"
            };
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            GetCustomerDetailQuery query = new GetCustomerDetailQuery(dbContext, mapper);
            query.CustomerId = customer.Id;

            // act
            var result = query.Handle();

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(customer.FirstName);
            result.LastName.Should().Be(customer.LastName);
            result.Email.Should().Be(customer.Email);
        }
    }
}
