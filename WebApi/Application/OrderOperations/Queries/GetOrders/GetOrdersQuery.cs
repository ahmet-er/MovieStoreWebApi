using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        public SoftDeleteOrderModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOrdersQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<OrdersViewModel> Handle()
        {
            var orders = _context.Orders.Where(x => x.IsDeleted == false && x.Id != 0).Include(x => x.Movie).Include(x => x.Customer);
            
            List<OrdersViewModel> vm = _mapper.Map<List<OrdersViewModel>>(orders);
            return vm;
        }
        public class OrdersViewModel
        {
            public int Id { get; set; }
            public int CustomerId { get; set; }
            public int MovieId { get; set; }
            public double PurchasedPrice { get; set; }
            public DateTime PurchasedDate { get; set; }
        }
    }
}
