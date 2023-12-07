using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this ApplicationDbContext context)
        {
            context.Directors.AddRange
                (
                    new Director
                    {
                        Id = 1,
                        FirstName = "director 1",
                        LastName = "lname 1"
                    },
                    new Director
                    {
                        Id = 2,
                        FirstName = "director 2",
                        LastName = "lname 2"
                    },
                    new Director
                    {
                        Id = 3,
                        FirstName = "director 3",
                        LastName = "lname 3"
                    }
                );
        }
    }
}
