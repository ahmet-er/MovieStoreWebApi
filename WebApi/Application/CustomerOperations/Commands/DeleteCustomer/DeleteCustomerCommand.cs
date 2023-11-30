using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommand(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("No customer to delete was found.");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
