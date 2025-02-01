namespace GardenCentreApp;

public partial class ShoppingCart : ContentPage
{
    private readonly int currentUserID;
    public static List<CartItem> CartItems { get; set; } = new List<CartItem>();
    private decimal totalAmount = 0;

    public ShoppingCart(int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        LoadCartItems();
    }

    private void LoadCartItems()
    {
        CartItemsLayout.Children.Clear();
        totalAmount = 0;

        // Only show items belonging to current user
        var userItems = CartItems.Where(item => item.UserID == currentUserID).ToList();

        foreach (var item in userItems)
        {
            var frame = CreateCartItemFrame(item);
            CartItemsLayout.Children.Add(frame);
            totalAmount += item.Total;
        }

        UpdateTotal();
        CheckoutButton.IsEnabled = userItems.Any();
    }

    private Frame CreateCartItemFrame(CartItem item)
    {
        var frame = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 10,
            Padding = new Thickness(15)
        };

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        var stackLayout = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    Text = item.ProductName,
                    FontSize = 20,
                    TextColor = Color.FromArgb("#047B4D")
                },
                new Label
                {
                    Text = $"Quantity: {item.Quantity}",
                    FontSize = 16
                },
                new Label
                {
                    Text = $"€{item.Total:F2}",
                    FontSize = 16
                }
            }
        };

        var removeButton = new Button
        {
            Text = "Remove",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center
        };

        removeButton.Clicked += (s, e) => RemoveItem(item);

        grid.Add(stackLayout, 0);
        grid.Add(removeButton, 1);
        frame.Content = grid;

        return frame;
    }

    private void RemoveItem(CartItem item)
    {
        CartItems.Remove(item);
        LoadCartItems();
    }

    private void UpdateTotal()
    {
        TotalLabel.Text = $"Total: €{totalAmount:F2}";
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        var userItems = CartItems.Where(item => item.UserID == currentUserID).ToList();

        var summary = "Purchase Summary:\n\n";
        foreach (var item in userItems)
        {
            summary += $"{item.ProductName}\n";
            summary += $"Quantity: {item.Quantity}\n";
            summary += $"Price: €{item.Total:F2}\n\n";
        }
        summary += $"\nTotal Amount: €{totalAmount:F2}";

        await DisplayAlert("Checkout Complete", summary, "OK");

        // Remove checked out items
        foreach (var item in userItems.ToList())
        {
            CartItems.Remove(item);
        }

        // Navigate back to previous page instead of root
        await Navigation.PopAsync();
    }

    private bool isLoggingOut = false;

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        if (isLoggingOut) return;

        isLoggingOut = true;
        try
        {
            bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                CartItems.Clear();
                await Navigation.PopToRootAsync();
            }
        }
        finally
        {
            isLoggingOut = false;
        }
    }
}
