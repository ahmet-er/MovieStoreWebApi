using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var movie = _context.Movies.Include(x => x.MovieActors).SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Cannot find the movie.");

            _mapper.Map(Model, movie);
            _context.SaveChanges();
        }
    }
    public class UpdateMovieModel
    {
        public DateTime PublishDate { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public double Price { get; set; }
    }
}
