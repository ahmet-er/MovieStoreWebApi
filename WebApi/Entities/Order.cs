namespace WebApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer PurchasingCustomer { get; set; }
        public int MovieId { get; set; }
        public Movie PurchasedMovie { get; set; }
        public double PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}
