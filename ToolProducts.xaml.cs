namespace GardenCentreApp;

public partial class ToolProducts : ContentPage
{
    private Dictionary<string, (int Quantity, decimal Price)> products;
    private bool isCartOpening = false;
    private int currentUserID;

    public ToolProducts(int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        products = new Dictionary<string, (int, decimal)>(); // Initialize empty dictionary
        InitializeProducts();
    }

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
                                UserID = currentUserID  // Add the UserID to track ownership
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
