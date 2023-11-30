using AutoMapper;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public CreateTokenCommand(IApplicationDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }
        public Token Handle()
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (customer is not null)
            {
                TokenHandler handler = new TokenHandler(_config);
                Token token = handler.CreateAccessToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
                throw new InvalidOperationException("Ooops.. Wrong Mail - Password.");
        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
