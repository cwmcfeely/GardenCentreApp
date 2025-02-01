namespace GardenCentreApp;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage(string userName)
    {
        InitializeComponent();
        WelcomeLabel.Text = $"Welcome {userName}, choose a shopping category";
        CartButton.Clicked += OnCartClicked;
        LogoutButton.Clicked += OnLogoutClicked;
        PlantsCategory.Tapped += OnCategoryTapped;
        ToolsCategory.Tapped += OnCategoryTapped;
        GardenCareCategory.Tapped += OnCategoryTapped;
    }

    private async void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        if (sender is TapGestureRecognizer tap)
        {
            var frame = tap.Parent as Frame;
            var grid = frame.Content as Grid;
            var stack = grid.Children[1] as VerticalStackLayout;
            var categoryLabel = stack.Children[0] as Label;

            string categoryName = categoryLabel.Text;
            await DisplayAlert("Category Selected", $"You selected {categoryName}", "OK");
        }
    }

    private bool isCartOpening = false;  // Add this field at class level

    private async void OnCartClicked(object sender, EventArgs e)
    {
        if (isCartOpening) return;  // Prevent multiple executions

        isCartOpening = true;
        try
        {
            await DisplayAlert("Cart", "Shopping Cart clicked", "OK");
        }
        finally
        {
            isCartOpening = false;  // Reset the flag
        }
    }

    private bool isLoggingOut = false;  // Add this field at class level

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        if (isLoggingOut) return;  // Prevent multiple executions

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
            isLoggingOut = false;  // Reset the flag
        }
    }


    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
