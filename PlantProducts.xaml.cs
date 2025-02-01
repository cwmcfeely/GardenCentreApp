namespace GardenCentreApp;

/// Page that displays and manages plant products available for purchase
public partial class PlantProducts : ContentPage
{

    /// Dictionary storing product information: product name as key, quantity and price as value tuple
    private Dictionary<string, (int Quantity, decimal Price)> products;
    /// Flag to prevent multiple cart pages from being opened at the same time
    private bool isCartOpening = false;
    /// Stores the ID of the currently logged-in user
    private int currentUserID;

    /// Initializes a new instance of the PlantProducts page
    public PlantProducts(int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        products = new Dictionary<string, (int, decimal)>();
        InitializeProducts();
    }

    /// Initializes the product catalog with default prices and zero quantities
    private void InitializeProducts()
    {
        products = new Dictionary<string, (int, decimal)>
        {
            { "Succulent", (0, 15.99m) },
            { "Euclidean", (0, 24.99m) },
            { "Violet", (0, 12.99m) },
            { "Yucca", (0, 29.99m) },
            { "Petunia", (0, 9.99m) }
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
                else
                {
                    priceLabel.Text = $"€{totalPrice:F2} total";
                }
            }
        }
    }

    /// Handles adding products to the shopping cart
    private async void OnAddToCartClicked(object? sender, EventArgs e)
    {
        try
        {
            if (sender is Button button)
            {
                var productName = button.CommandParameter?.ToString();
                if (!string.IsNullOrEmpty(productName))
                {
                    var quantityLabel = this.FindByName<Label>($"{productName}Quantity");
                    if (quantityLabel != null)
                    {
                        int quantity = int.Parse(quantityLabel.Text);
                        if (quantity > 0)
                        {
                            var price = products[productName].Price;
                            ShoppingCart.CartItems.Add(new CartItem
                            {
                                ProductName = productName,
                                Quantity = quantity,
                                Price = price,
                                UserID = currentUserID
                            });

                            await DisplayAlert("Added to Cart",
                                $"Added {quantity} {productName}(s) to your cart", "OK");
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Failed to add item to cart", "OK");
        }
    }

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
