using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _context.Directors.Include(x => x.Movies).SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);
            if (director is not null)
                throw new InvalidOperationException("The director has already in db.");

            director = _mapper.Map<Director>(Model);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }
    public class CreateDirectorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
