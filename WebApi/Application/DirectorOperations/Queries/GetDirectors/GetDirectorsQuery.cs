using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorsQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<DirectorsViewModel> Handle()
        {
            var directors = _context.Directors.Include(x => x.Movies).Where(x => x.Id != 0).OrderBy(x => x.Id).ToList();
            List<DirectorsViewModel> returnObj = _mapper.Map<List<DirectorsViewModel>>(directors);
            return returnObj;
        }
    }
    public class DirectorsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
