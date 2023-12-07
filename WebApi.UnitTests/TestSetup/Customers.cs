using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Customers
    {
        public static void AddCustomers(this ApplicationDbContext context)
        {
            context.Customers.AddRange
                (
                    new Customer
                    {
                        Id = 1,
                        FirstName = "John 1",
                        LastName = "Doe 1",
                        Email = "john.doe1@example.com",
                        Password = "12345",
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "John 2",
                        LastName = "Doe 2",
                        Email = "john.doe2@example.com",
                        Password = "12345",
                    },
                    new Customer
                    {
                        Id = 3,
                        FirstName = "John 3",
                        LastName = "Doe 3",
                        Email = "john.doe3@example.com",
                        Password = "12345",
                    }
                );
        }
    }
}
