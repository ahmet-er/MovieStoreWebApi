namespace WebApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public double PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
