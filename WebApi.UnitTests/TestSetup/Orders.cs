using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Orders
    {
        public static void AddOrders(this ApplicationDbContext context)
        {
            context.Orders.AddRange
                (
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        IsDeleted = false,
                        MovieId = 1,
                        PurchasedDate = DateTime.Now.AddDays(-10),
                        PurchasedPrice = 11,
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 2,
                        IsDeleted = false,
                        MovieId = 2,
                        PurchasedDate = DateTime.Now.AddDays(-20),
                        PurchasedPrice = 22,
                    },
                    new Order
                    {
                        Id = 3,
                        CustomerId = 3,
                        IsDeleted = false,
                        MovieId = 3,
                        PurchasedDate = DateTime.Now.AddDays(-30),
                        PurchasedPrice = 33,
                    }
                );
        }
    }
}
