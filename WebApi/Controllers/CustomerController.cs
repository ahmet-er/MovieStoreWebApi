using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public CustomerController(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = newCustomer;
            command.Handle();

            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.Validate(command);

            return Ok("The customer created successfully.");
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> ResfreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }
    }
}
