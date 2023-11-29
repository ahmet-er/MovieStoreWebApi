using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;
        public ActorController(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_context, _mapper);
            var obj = query.Handle();

            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetActorDetail(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = id;


            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public ActionResult AddActor([FromBody] CreateActorModel newActor)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = newActor;

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The Actor created successfully.");
        }

        [HttpPut("id")]
        public ActionResult UpdateActor(int id, [FromBody] UpdateActorModel updateActor)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            command.ActorId = id;
            command.Model = updateActor;

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The Actor updated successfully.");
        }

        [HttpDelete("id")]
        public ActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("The Actor deleted successfully.");
        }
    }
}
