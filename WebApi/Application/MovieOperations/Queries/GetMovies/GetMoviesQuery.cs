using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MoviesViewModel> Handle()
        {
            var movies = _context.Movies.Where(x => x.Id != 0).OrderBy(x => x.Id).Include(x => x.MovieActors);
            List<MoviesViewModel> returnObj = _mapper.Map<List<MoviesViewModel>>(movies);
            return returnObj;
        }
    }
    public class MoviesViewModel
    {
        public DateTime PublishDate { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public double Price { get; set; }
    }
}
