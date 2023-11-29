using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IApplicationDbContext
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Log> Logs { get; set; }
        int SaveChanges();
    }
}
