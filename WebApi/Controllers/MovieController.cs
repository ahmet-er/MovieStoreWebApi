using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetMovies()
        {
            GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
            var obj = query.Handle();

            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetMovieDetail(int id)
        {
            GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
            query.MovieId = id;

            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public ActionResult AddMovie([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = newMovie;

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The movie created successfully.");
        }

        [HttpPut("id")]
        public ActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updateModel)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.Model = updateModel;
            command.MovieId = id;

            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The movie updated successfully.");
        }

        [HttpDelete("id")]
        public ActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = id;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The movie deleted successfully.");
        }
    }
}
