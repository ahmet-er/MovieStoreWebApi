using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        public UpdateActorModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.Include(x => x.MovieActors).SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("No actor to update was found.");

            _mapper.Map(Model, actor);
            _context.SaveChanges();
        }
    }
    public class UpdateActorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
