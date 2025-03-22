namespace GardenCentreApp;

/// Main application class that handles app initialization and database access
public partial class App : Application
{
	// Static instance of the database to ensure single database connection
	static UserDatabase? database;

	/// Public property that provides access to the user database
	/// database is only created when first accessed
	public static UserDatabase Database
	{
		get
		{
			// Check if database instance exists
			if (database == null)
			{
				// Create new database instance if none exists
				// Store the database file in the app's data directory
				database = new UserDatabase(Path.Combine(FileSystem.AppDataDirectory, "Users.db3"));
			}
			return database;
		}
	}

	/// Constructor for the App class Initializes the application and sets up the navigation
	public App()
	{
		// Initialize the app components
		InitializeComponent();

		// Set the main page as a NavigationPage containing MainPage
		// Configure navigation bar appearance with custom colors
		MainPage = new NavigationPage(new MainPage())
		{
			// Set navigation bar background color to green (#047B4D)
			BarBackgroundColor = Color.FromArgb("#047B4D"),
			// Set navigation bar text color to white
			BarTextColor = Colors.White
		};
	}
}
