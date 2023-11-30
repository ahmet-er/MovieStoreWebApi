using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomersQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<CustomersViewModel> Handle()
        {
            var customerList = _context.Customers.Include(x => x.PurchasedMovies).Include(x => x.FavoriteGenres).OrderBy(x => x.Id).ToList<Customer>();
            List<CustomersViewModel> vm = _mapper.Map<List<CustomersViewModel>>(customerList);

            return vm;
        }
    }
    public class CustomersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Order> PurchasedMovies { get; set; }
        public ICollection<Genre> FavoriteGenres { get; set; }
    }
}
