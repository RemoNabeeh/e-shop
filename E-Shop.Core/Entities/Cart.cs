namespace E_Shop.Core.Entities
{
    public class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
