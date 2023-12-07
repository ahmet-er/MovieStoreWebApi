using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        public int CustomerId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetCustomerDetailViewModel Handle()
        {
            var customer = _context.Customers.Include(x => x.FavoriteGenres).Include(x => x.Orders).Where(x => x.Id == CustomerId).FirstOrDefault();
            GetCustomerDetailViewModel vm = _mapper.Map<GetCustomerDetailViewModel>(customer);

            return vm;
        }
    }
    public class GetCustomerDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
