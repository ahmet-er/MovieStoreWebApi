using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.Include(x => x.MovieActors).SingleOrDefault(x => x.FirstName.Trim().ToLower() == Model.FirstName.Trim().ToLower() && x.LastName.Trim().ToLower() == Model.LastName.Trim().ToLower());
            if (actor is not null)
                throw new InvalidOperationException("This actor is already registered in the database.");
            actor = _mapper.Map<Actor>(Model);

            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }
    public class CreateActorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
