namespace GardenCentreApp;

/// Main login page of the application that handles user authentication
public partial class MainPage : ContentPage
{

	/// Initializes the main page and sets up event handlers for login and signup
	public MainPage()
	{
		InitializeComponent();
		LoginButton.Clicked += OnLoginClicked;
		SignUpTap.Tapped += OnSignUpTapped;
	}

	/// Handles navigation to the sign up page when the sign up text is tapped
	private async void OnSignUpTapped(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new SignUpPage());
	}

	/// Validates user credentials and navigates to categories page on success
	private async void OnLoginClicked(object? sender, EventArgs e)
	{
		// Validate that both email and password fields are filled
		if (string.IsNullOrEmpty(EmailEntry.Text) ||
			string.IsNullOrEmpty(PasswordEntry.Text))
		{
			await DisplayAlert("Error", "Please enter both email and phone number", "OK");
			return;
		}

		// Attempt to retrieve user from database using provided credentials
		var user = await App.Database.GetUserAsync(EmailEntry.Text, PasswordEntry.Text);
		if (user != null)
		{
			// On successful login, navigate to categories page with user info
			var userName = user.UserName ?? string.Empty;
			await Navigation.PushAsync(new CategoriesPage(user.UserName, user.UserID));

			// Clear login fields for security
			EmailEntry.Text = string.Empty;
			PasswordEntry.Text = string.Empty;
		}
		else
		{
			// Display error message for invalid credentials
			await DisplayAlert("Error", "Invalid email or phone number", "OK");
		}
	}
}
