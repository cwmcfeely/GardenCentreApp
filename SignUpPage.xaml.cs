namespace GardenCentreApp;

/// Page that handles new user registration
public partial class SignUpPage : ContentPage
{
    /// Initializes a new instance of the SignUpPage, Sets up event handlers for buttons and controls
    public SignUpPage()
    {
        InitializeComponent();
        SignUpButton.Clicked += OnSignUpClicked;
        LoginTap.Tapped += OnLoginTapped;
        CorporateCheckbox.CheckedChanged += OnCorporateCheckboxChanged;
    }

    /// Shows or hides corporate-specific fields based on checkbox state
    private void OnCorporateCheckboxChanged(object? sender, CheckedChangedEventArgs e)
    {
        CorporateFields.IsVisible = e.Value;
    }

    /// Handles the sign up process when the sign up button is clicked
    /// Validates user input and creates new user account
    private async void OnSignUpClicked(object? sender, EventArgs e)
    {
        // Validate required fields
        if (string.IsNullOrEmpty(NameEntry.Text) ||
            string.IsNullOrEmpty(EmailEntry.Text) ||
            string.IsNullOrEmpty(PhoneEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all required fields", "OK");
            return;
        }

        // Create new user object with form data
        var user = new User
        {
            UserName = NameEntry.Text,
            Email = EmailEntry.Text,
            PhoneNumber = PhoneEntry.Text,
            CustomerType = CorporateCheckbox.IsChecked ? "Corporate" : "General"
        };

        try
        {
            // Save user to database and return to login page
            await App.Database.SaveUserAsync(user);
            await DisplayAlert("Success", "Account created successfully!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Failed to create account", "OK");
        }
    }

    /// Handles navigation back to login page when login text is tapped
    private async void OnLoginTapped(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
