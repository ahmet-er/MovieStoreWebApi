using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DirectorController(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] 
        public ActionResult GetDirectors()
        {
            GetDirectorsQuery query = new GetDirectorsQuery(_context, _mapper);
            var obj = query.Handle();

            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetDirectorDetail(int id)
        {
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
            query.DirectorId = id;

            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public ActionResult AddDirector([FromBody] CreateDirectorModel newDirector)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            command.Model = newDirector;

            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The director created successfully.");
        }

        [HttpPut("id")]
        public ActionResult UpdateDirector(int id, [FromBody] UpdateDirectorModel updateDirector)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
            command.DirectorId = id;
            command.Model = updateDirector;

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The Director updated successfully.");
        }

        [HttpDelete("id")]
        public ActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = id;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The director deleted successfully.");
        }
    }
}
