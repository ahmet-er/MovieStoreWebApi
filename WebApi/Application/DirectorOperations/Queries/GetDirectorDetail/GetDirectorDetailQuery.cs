using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        public int DirectorId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = _context.Directors.Include(x => x.Movies).SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Cannot found the director");

            return _mapper.Map<DirectorDetailViewModel>(director);
        }
    }
    public class DirectorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
