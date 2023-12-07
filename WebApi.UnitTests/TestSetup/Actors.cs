using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this ApplicationDbContext context)
        {
            context.Actors.AddRange
                (
                    new Actor
                    {
                        Id = 1,
                        FirstName = "first",
                        LastName = "first"
                    },
                    new Actor
                    {
                        Id = 2,
                        FirstName = "second",
                        LastName = "second"
                    },
                    new Actor
                    {
                        Id = 3,
                        FirstName = "third",
                        LastName = "third"
                    }
                );
        }
    }
}
