using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IConfiguration _config;
        public RefreshTokenCommand(IApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public Token Handle()
        {
            var customer = _context.Customers.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
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
                throw new InvalidOperationException("Cannot found a valid refresh token");
        }
    }
}
