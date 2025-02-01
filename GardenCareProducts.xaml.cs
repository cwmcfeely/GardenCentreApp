namespace GardenCentreApp;

public partial class GardenCareProducts : ContentPage
{
    private Dictionary<string, (int Quantity, decimal Price)> products;
    private bool isCartOpening = false;

    public GardenCareProducts()
    {
        InitializeComponent();
        InitializeProducts();
    }

    private void InitializeProducts()
    {
        products = new Dictionary<string, (int, decimal)>
        {
            { "GardenSoil", (0, 9.99m) },
            { "Fertiliser", (0, 17.99m) },
            { "WeedKiller", (0, 29.99m) }
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
                    priceLabel.Text = $"€{totalPrice:F2}";
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
        if (sender is Button button)
        {
            var productName = button.CommandParameter?.ToString();
            if (productName != null)
            {
                var quantityLabel = this.FindByName<Label>($"{productName}Quantity");
                if (quantityLabel != null)
                {
                    int quantity = int.Parse(quantityLabel.Text);
                    if (quantity > 0)
                    {
                        await DisplayAlert("Added to Cart",
                            $"Added {quantity} {productName}(s) to your cart", "OK");
                    }
                }
            }
        }
    }

    private async void OnCartClicked(object sender, EventArgs e)
    {
        if (isCartOpening) return;

        isCartOpening = true;
        try
        {
            await DisplayAlert("Cart", "Shopping Cart clicked", "OK");
        }
        finally
        {
            isCartOpening = false;
        }
    }
}
