using AutoMapper;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        public int OrderId { get; set; }
        public UpdateOrderModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateOrderCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == OrderId);

            if (order is null)
                throw new InvalidOperationException("There is no order in db");

            _mapper.Map(Model, order);
            _context.SaveChanges();
        }
    }
    public class UpdateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public double PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}
