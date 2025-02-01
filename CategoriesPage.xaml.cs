namespace GardenCentreApp;

/// Page that displays different shopping categories for the garden centre
public partial class CategoriesPage : ContentPage
{
    /// Stores the ID of the currently logged-in user
    private readonly int currentUserID;

    /// Initializes a new instance of the CategoriesPage
    public CategoriesPage(string userName, int userID)
    {
        InitializeComponent();
        currentUserID = userID;
        // Set welcome message with user's name
        WelcomeLabel.Text = $"Welcome {userName}, choose a shopping category";
        // Register click event handlers for cart and logout buttons
        CartButton.Clicked += OnCartClicked;
        LogoutButton.Clicked += OnLogoutClicked;
    }

    /// Navigates to the appropriate product page based on the selected category
    private async void OnCategoryTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            // Navigate through the visual tree to find the category label
            var grid = frame.Content as Grid;
            var stack = grid?.Children[1] as VerticalStackLayout;
            var categoryLabel = stack?.Children[0] as Label;

            // Navigate to appropriate product page based on category
            if (categoryLabel?.Text == "Plants")
            {
                await Navigation.PushAsync(new PlantProducts(currentUserID));
            }
            else if (categoryLabel?.Text == "Tools")
            {
                await Navigation.PushAsync(new ToolProducts(currentUserID));
            }
            else if (categoryLabel?.Text == "Garden Care")
            {
                await Navigation.PushAsync(new GardenCareProducts(currentUserID));
            }
            else
            {
                await DisplayAlert("Error", "Invalid category", "OK");
            }
        }
    }

    /// Flag to prevent multiple cart pages from being opened at the same time
    private bool isCartOpening = false;

    /// Navigates to the shopping cart page
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

    /// Flag to prevent multiple logout attempts simultaneously
    private bool isLoggingOut = false;
    /// Displays confirmation dialog and returns to root page if confirmed
    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        if (isLoggingOut) return;

        isLoggingOut = true;
        try
        {
            bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                await Navigation.PopToRootAsync();
            }
        }
        finally
        {
            isLoggingOut = false;
        }
    }
}
