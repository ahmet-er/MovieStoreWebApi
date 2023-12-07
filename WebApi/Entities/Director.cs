using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Movie> Movies { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
