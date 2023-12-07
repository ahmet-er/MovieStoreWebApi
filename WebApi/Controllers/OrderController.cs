using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderController(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetOrders()
        {
            GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
            var obj = query.Handle();

            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetOrderDetail(int id)
        {
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = id;

            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public ActionResult AddOrder([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = newOrder;

            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The order created successfully.");
        }

        [HttpPut("id")]
        public ActionResult UpdateOrder(int id, [FromBody] UpdateOrderModel updateModel)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_context, _mapper);
            command.OrderId = id;
            command.Model = updateModel;

            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The order updated successfully.");
        }

        [HttpPost("id")]
        public ActionResult SoftDeleteOrder(int id, [FromBody] SoftDeleteOrderModel softDeleteModel)
        {
            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(_context, _mapper);
            command.OrderId = id;
            command.Model = softDeleteModel;

            SoftDeleteOrderCommandValdiator validator = new SoftDeleteOrderCommandValdiator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The order soft deleted successfully.");
        }
    }
}
