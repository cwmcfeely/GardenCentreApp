namespace GardenCentreApp;

public partial class PlantProducts : ContentPage
{
    private Dictionary<string, (int Quantity, decimal Price)> products;
    private bool isCartOpening = false;

    public PlantProducts()
    {
        InitializeComponent();
        InitializeProducts();
    }

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

    private void OnIncrementClicked(object sender, EventArgs e)
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

    private void OnDecrementClicked(object sender, EventArgs e)
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
                    priceLabel.Text = $"€{totalPrice:F2} total";
                }
                else
                {
                    priceLabel.Text = $"€{totalPrice:F2} total";
                }
            }
        }
    }

    private async void OnAddToCartClicked(object sender, EventArgs e)
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
                                Price = price
                            });

                            await DisplayAlert("Added to Cart",
                                $"Added {quantity} {productName}(s) to your cart", "OK");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to add item to cart", "OK");
        }
    }


    private async void OnCartClicked(object sender, EventArgs e)
    {
        if (isCartOpening) return;

        isCartOpening = true;
        try
        {
            await Navigation.PushAsync(new ShoppingCart());
        }
        finally
        {
            isCartOpening = false;
        }
    }
}
