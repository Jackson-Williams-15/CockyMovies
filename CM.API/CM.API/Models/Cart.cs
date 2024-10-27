public class Cart
{
    public int CartId { get; set; }
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}