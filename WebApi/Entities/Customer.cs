using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public ICollection<Order> PurchasedMovies { get; set; }
        public ICollection<Genre> FavoriteGenres { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
