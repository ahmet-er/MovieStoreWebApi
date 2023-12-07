using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Services
{
    public class DbLogger
    {
        private readonly ApplicationDbContext _context;
        public DbLogger(ApplicationDbContext context)
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
