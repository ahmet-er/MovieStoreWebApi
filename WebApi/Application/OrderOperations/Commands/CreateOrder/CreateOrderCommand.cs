using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public CreateOrderModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x => x.CustomerId == Model.CustomerId && x.MovieId == Model.MovieId);

            if (order is not null)
                throw new InvalidOperationException("You buyed this movie before.");

            order = _mapper.Map<Order>(Model);

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public double PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; } = DateTime.Now;
    }
}
