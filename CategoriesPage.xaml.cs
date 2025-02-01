namespace GardenCentreApp;

public partial class CategoriesPage : ContentPage
{
    private readonly int currentUserID;
    public CategoriesPage(string userName, int userID)
    {
        InitializeComponent();
        currentUserID = userID;
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


    private bool isCartOpening = false;

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


    private bool isLoggingOut = false;

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
