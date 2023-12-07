using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this ApplicationDbContext context)
        {
            context.Genres.AddRange
                (
                    new Genre { Id = 1, Name = "Drama" },
                    new Genre { Id = 2, Name = "Comedy" },
                    new Genre { Id = 3, Name = "Action" }
                );
        }
    }
}
