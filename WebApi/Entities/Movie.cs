namespace WebApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public Genre Genre { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public double Price { get; set; }
    }
}
