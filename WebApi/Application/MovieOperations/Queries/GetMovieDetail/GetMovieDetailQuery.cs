using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        public int MovieId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MovieDetailViewModel Handle()
        {
            var movie = _context.Movies.Include(x => x.MovieActors).SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Cannot find the movie");

            return _mapper.Map<MovieDetailViewModel>(movie);
        }
    }
    public class MovieDetailViewModel
    {
        public DateTime PublishDate { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public double Price { get; set; }
    }
}
