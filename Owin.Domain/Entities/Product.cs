namespace Owin.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
    }
}
