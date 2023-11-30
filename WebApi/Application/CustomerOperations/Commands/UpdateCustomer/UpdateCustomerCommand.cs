using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        public int CustomerId { get; set; }
        public UpdateCustomerModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCustomerCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("No cusstomer to update was found.");

            _mapper.Map(Model, customer);
            _context.SaveChanges();
        }
    }
    public class UpdateCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
