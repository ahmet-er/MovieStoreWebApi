﻿using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly IApplicationDbContext _context;

        public DeleteMovieCommand(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Cannot found the movie.");

            if (movie.MovieActors is not null && movie.MovieActors.Any())
                throw new InvalidOperationException("Cannot delete beacuse movie have actors.");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
