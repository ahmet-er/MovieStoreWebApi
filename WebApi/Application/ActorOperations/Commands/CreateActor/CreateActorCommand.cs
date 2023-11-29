using AutoMapper;
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
            var actor = _context.Actors.SingleOrDefault(x => x.FullName.ToLower() == Model.FullName.ToLower());
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
        public ICollection<MovieActor> MovieActors { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
