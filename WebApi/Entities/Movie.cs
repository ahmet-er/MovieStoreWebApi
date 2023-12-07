namespace WebApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public double Price { get; set; }
    }
}
