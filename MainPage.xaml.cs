namespace GardenCentreApp;
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		LoginButton.Clicked += OnLoginClicked;
		SignUpTap.Tapped += OnSignUpTapped;
	}

	private async void OnSignUpTapped(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new SignUpPage());
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(EmailEntry.Text) ||
			string.IsNullOrEmpty(PasswordEntry.Text))
		{
			await DisplayAlert("Error", "Please enter both email and phone number", "OK");
			return;
		}

		var user = await App.Database.GetUserAsync(EmailEntry.Text, PasswordEntry.Text);
		if (user != null)
		{
			await DisplayAlert("Success", "Login successful!", "OK");
			// Navigate to CategoriesPage with user's name
			await Navigation.PushAsync(new CategoriesPage(user.UserName));

			// Clear entry fields after successful login
			EmailEntry.Text = string.Empty;
			PasswordEntry.Text = string.Empty;
		}
		else
		{
			await DisplayAlert("Error", "Invalid email or phone number", "OK");
		}
	}
}
