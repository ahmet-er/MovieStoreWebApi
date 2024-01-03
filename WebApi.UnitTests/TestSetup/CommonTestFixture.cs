using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public ApplicationDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieStoreDB")
                .Options;
            Context = new ApplicationDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddActors();
            Context.AddCustomers();
            Context.AddDirectors();
            Context.AddGenres();
            Context.AddMovieActors();
            Context.AddMovies();
            Context.AddOrders();
            Context.SaveChanges();

            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = configuration;

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
