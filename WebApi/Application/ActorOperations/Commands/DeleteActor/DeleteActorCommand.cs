using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IApplicationDbContext _context;
        public DeleteActorCommand(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("No actor to delete was found.");

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }
    }
}
