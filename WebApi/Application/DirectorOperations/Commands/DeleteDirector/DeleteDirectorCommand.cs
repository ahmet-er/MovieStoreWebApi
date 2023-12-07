using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IApplicationDbContext _context;

        public DeleteDirectorCommand(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.Include(x => x.Movies).SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Cannot found the director.");

            if (director.Movies.Any())
                throw new InvalidOperationException("You can't delete this director beacuse this director has a movie.");

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }
    }
}
