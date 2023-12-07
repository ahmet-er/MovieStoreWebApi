using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommand(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _context.Movies.Include(x => x.MovieActors).SingleOrDefault(x => x.Name.Trim().ToLower() == Model.Name.Trim().ToLower());
            if (movie is not null)
                throw new InvalidOperationException("This movie already have db.");

            movie = _mapper.Map<Movie>(Model);

            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
    }
    public class CreateMovieModel
    {
        public DateTime PublishDate { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public double Price { get; set; }
    }
}
