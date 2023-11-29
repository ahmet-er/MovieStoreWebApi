using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }
        public GetActorDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetActorDetailViewModel Handle()
        {
            var actor = _context.Actors.Include(x => x.MovieActors).Where(actor => actor.Id == ActorId).SingleOrDefault();
            GetActorDetailViewModel vm = _mapper.Map<GetActorDetailViewModel>(actor);

            return vm;
        }
    }
    public class GetActorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
