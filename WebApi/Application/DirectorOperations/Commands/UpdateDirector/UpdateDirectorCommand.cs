using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public UpdateDirectorModel Model { get; set; }
        public int DirectorId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateDirectorCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _context.Directors.Include(x => x.Movies).SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Cannot found the director");

            _mapper.Map(Model, director);
            _context.SaveChanges();
        }
    }
    public class UpdateDirectorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
