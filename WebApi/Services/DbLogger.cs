using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Services
{
    public class DbLogger : ILoggerService
    {
        private readonly IApplicationDbContext _context;
        public DbLogger(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Write(string message)
        {
            var log = new Log
            {
                Message = message,
                LogDate = DateTime.Now
            };

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
