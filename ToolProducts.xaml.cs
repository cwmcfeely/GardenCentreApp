namespace GardenCentreApp;

/// Page that displays and manages gardening tool products available for purchase
public partial class ToolProducts : ContentPage
{
    /// Dictionary storing product information: product name as key, quantity and price as value tuple
    private Dictionary<string, (int Quantity, decimal Price)> products;

    /// Flag to prevent multiple cart pages from being opened at the same time
    private bool isCartOpening = false;
    /// Stores the ID of the currently logged-in user
    private int currentUserID;

    /// Initializes a new instance of the ToolProducts page
    public ToolProducts(int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        products = new Dictionary<string, (int, decimal)>();
        InitializeProducts();
    }

    /// Initializes the product catalog with default prices
    private void InitializeProducts()
    {
        products = new Dictionary<string, (int, decimal)>
        {
            { "Rake", (0, 25.99m) },
            { "HandTools", (0, 17.99m) },
            { "GardenHose", (0, 29.99m) },
            { "Spade", (0, 23.99m) },
            { "Shears", (0, 21.99m) }
        };
    }

    /// Handles the increment button click event to increase product quantity
    private void OnIncrementClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var parent = button.Parent as StackLayout;
            if (parent != null)
            {
                var quantityLabel = parent.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null)
                {
                    int currentQuantity = int.Parse(quantityLabel.Text);
                    quantityLabel.Text = (currentQuantity + 1).ToString();
                    UpdateTotalPrice();
                }
            }
        }
    }

    /// Handles the decrement button click event to decrease product quantity
    private void OnDecrementClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var parent = button.Parent as StackLayout;
            if (parent != null)
            {
                var quantityLabel = parent.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null)
                {
                    int currentQuantity = int.Parse(quantityLabel.Text);
                    if (currentQuantity > 0)
                    {
                        quantityLabel.Text = (currentQuantity - 1).ToString();
                        UpdateTotalPrice();
                    }
                }
            }
        }
    }

    /// Updates the displayed price for all products based on their quantities
    /// Handles different display formats for single items vs multiple items
    private void UpdateTotalPrice()
    {
        foreach (var product in products)
        {
            var quantityLabel = this.FindByName<Label>($"{product.Key}Quantity");
            var priceLabel = this.FindByName<Label>($"{product.Key}Price");

            if (quantityLabel != null && priceLabel != null)
            {
                int quantity = int.Parse(quantityLabel.Text);
                decimal basePrice = product.Value.Price;
                decimal totalPrice = basePrice * quantity;

                if (quantity == 0)
                {
                    priceLabel.Text = $"€{basePrice:F2} each";
                }
                else if (quantity == 1)
                {
                    priceLabel.Text = $"€{totalPrice:F2}";
                }
                else
                {
                    priceLabel.Text = $"€{totalPrice:F2} total";
                }
            }
        }
    }

    /// Handles the event when a user clicks to add an item to their shopping cart
    /// Validates input, creates a cart item, and provides user feedback
    private async void OnAddToCartClicked(object? sender, EventArgs e)
    {
        // Wrap the entire operation in a try-catch block to handle any potential errors
        try
        {
            // Verify that the sender is a Button control and cast it
            if (sender is Button button)
            {
                // Attempt to get the product name from the button's CommandParameter
                // Using null conditional operator (?.) to safely handle null values
                var productName = button.CommandParameter?.ToString();

                // Proceed only if we have a valid product name
                if (!string.IsNullOrEmpty(productName))
                {
                    // Find the quantity label in the UI using the product name
                    // Format: "{productName}Quantity" matches the naming convention in XAML
                    var quantityLabel = this.FindByName<Label>($"{productName}Quantity");

                    // Proceed only if we found the quantity label
                    if (quantityLabel != null)
                    {
                        // Convert the quantity text to an integer
                        int quantity = int.Parse(quantityLabel.Text);

                        // Only add to cart if quantity is greater than zero
                        if (quantity > 0)
                        {
                            // Get the product's price from our products dictionary
                            var price = products[productName].Price;

                            // Create new cart item and add it to the static cart items collection
                            ShoppingCart.CartItems.Add(new CartItem
                            {
                                ProductName = productName,
                                Quantity = quantity,
                                Price = price,
                                UserID = currentUserID  // Associate item with current user
                            });

                            // Show success message to user with item details
                            await DisplayAlert("Added to Cart",
                                $"Added {quantity} {productName}(s) to your cart", "OK");
                        }
                    }
                }
            }
        }
        // Catch any exceptions that occur during the process
        catch (Exception)
        {
            // Show error message to user if addition fails
            await DisplayAlert("Error", "Failed to add item to cart", "OK");
        }
    }

    /// Handles navigation to the shopping cart page
    /// Includes protection against multiple simultaneous navigation attempts
    private async void OnCartClicked(object? sender, EventArgs e)
    {
        if (isCartOpening) return;

        isCartOpening = true;
        try
        {
            await Navigation.PushAsync(new ShoppingCart(currentUserID));
        }
        finally
        {
            isCartOpening = false;
        }
    }
}
