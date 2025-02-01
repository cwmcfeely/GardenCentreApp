namespace GardenCentreApp;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
        SignUpButton.Clicked += OnSignUpClicked;
        LoginTap.Tapped += OnLoginTapped;
        CorporateCheckbox.CheckedChanged += OnCorporateCheckboxChanged;
    }

    private void OnCorporateCheckboxChanged(object? sender, CheckedChangedEventArgs e)
    {
        CorporateFields.IsVisible = e.Value;
    }

    private async void OnSignUpClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NameEntry.Text) ||
            string.IsNullOrEmpty(EmailEntry.Text) ||
            string.IsNullOrEmpty(PhoneEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all required fields", "OK");
            return;
        }

        var user = new User
        {
            UserName = NameEntry.Text,
            Email = EmailEntry.Text,
            PhoneNumber = PhoneEntry.Text,
            CustomerType = CorporateCheckbox.IsChecked ? "Corporate" : "General"
        };

        try
        {
            await App.Database.SaveUserAsync(user);
            await DisplayAlert("Success", "Account created successfully!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Failed to create account", "OK");
        }
    }

    private async void OnLoginTapped(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}