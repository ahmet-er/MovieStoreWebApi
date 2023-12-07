using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetActorsQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ActorViewModel> Handle()
        {
            var actorList = _context.Actors.Include(x => x.MovieActors).OrderBy(x => x.Id).ToList<Actor>();
            List<ActorViewModel> vm = _mapper.Map<List<ActorViewModel>>(actorList);

            return vm;
        }
        public class ActorViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName => $"{FirstName} {LastName}";
        }
    }
}
