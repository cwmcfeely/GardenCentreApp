/// Represents an item in a shopping cart with product details and quantity
public class CartItem
{
    /// Initialized as an empty string to avoid null reference exceptions
    public string ProductName { get; set; } = string.Empty;

    /// The quantity of the product in the cart
    public int Quantity { get; set; }

    /// The unit price of the product
    public decimal Price { get; set; }

    /// Multiplies Quantity by Price
    public decimal Total => Quantity * Price;

    /// Used to associate cart items with specific users
    public int UserID { get; set; }
}