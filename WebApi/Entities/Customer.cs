namespace WebApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Order> PurchasedMovies { get; set; }
        public ICollection<Genre> FavoriteGenres { get; set; }
    }
}
