using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery
    {
        public int OrderId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public OrderDetailViewModel Handle()
        {
            var order = _context.Orders.Include(x => x.Movie).Include(x => x.Customer).SingleOrDefault(x => x.Id == OrderId);
            OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(order);

            return vm;
        }
    }
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public double PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}
