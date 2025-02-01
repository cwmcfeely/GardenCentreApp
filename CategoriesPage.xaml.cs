namespace GardenCentreApp;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage(string userName)
    {
        InitializeComponent();
        WelcomeLabel.Text = $"Welcome {userName}, choose a shopping category";
        CartButton.Clicked += OnCartClicked;
        LogoutButton.Clicked += OnLogoutClicked;
    }

    private async void OnCategoryTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            var grid = frame.Content as Grid;
            var stack = grid?.Children[1] as VerticalStackLayout;
            var categoryLabel = stack?.Children[0] as Label;

            if (categoryLabel?.Text == "Plants")
            {
                await Navigation.PushAsync(new PlantProducts());
            }
            else if (categoryLabel?.Text == "Tools")
            {
                await Navigation.PushAsync(new ToolProducts());
            }
            else if (categoryLabel?.Text == "GardenCare")
            {
                await Navigation.PushAsync(new GardenCareProducts());
            }
            else
            {
                await DisplayAlert("Coming Soon", $"{categoryLabel?.Text} page is not yet implemented.", "OK");
            }
        }
    }

    private bool isCartOpening = false;

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
                await Navigation.PopToRootAsync();
            }
        }
        finally
        {
            isLoggingOut = false;
        }
    }

}
