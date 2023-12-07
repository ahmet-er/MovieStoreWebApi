using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class SoftDeleteOrderCommand
    {
        public int OrderId { get; set; }
        public SoftDeleteOrderModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SoftDeleteOrderCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == OrderId);

            if (order is null || order.IsDeleted == true)
                throw new InvalidOperationException("There is no order in db");

            _mapper.Map(Model, order);
            _context.SaveChanges();
        }
    }
    public class SoftDeleteOrderModel
    {
        public bool IsDeleted { get; set; }
    }
}
