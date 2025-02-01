namespace GardenCentreApp;

/// Shopping cart page that displays and manages items added by users
public partial class ShoppingCart : ContentPage
{
    /// Stores the ID of the currently logged-in user
    private readonly int currentUserID;
    /// Static list that stores all cart items across the application
    public static List<CartItem> CartItems { get; set; } = new List<CartItem>();
    /// Tracks the total amount of all items in the cart
    private decimal totalAmount = 0;

    /// Initializes a new instance of the ShoppingCart page
    public ShoppingCart(int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        LoadCartItems();
    }

    /// Loads and displays cart items belonging to the current user Updates total amount and checkout button state
    private void LoadCartItems()
    {
        CartItemsLayout.Children.Clear();
        totalAmount = 0;

        // Filter items for current user
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

    /// Creates a visual frame for displaying a cart item
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

    /// Removes an item from the cart and refreshes the display
    private void RemoveItem(CartItem item)
    {
        CartItems.Remove(item);
        LoadCartItems();
    }

    /// Updates the total amount display
    private void UpdateTotal()
    {
        TotalLabel.Text = $"Total: €{totalAmount:F2}";
    }

    /// Handles the checkout process when checkout button is clicked, Displays purchase summary and clears cart
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

        await Navigation.PopAsync();
    }

    /// Flag to prevent multiple logout attempts simultaneously
    private bool isLoggingOut = false;

    /// Handles the logout process when logout button is clicked, Clears cart and returns to login page
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
