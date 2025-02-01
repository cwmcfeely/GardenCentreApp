namespace GardenCentreApp;

public partial class App : Application
{
	static UserDatabase database;

	public static UserDatabase Database
	{
		get
		{
			if (database == null)
			{
				database = new UserDatabase(Path.Combine(FileSystem.AppDataDirectory, "Users.db3"));
			}
			return database;
		}
	}

	public App()
	{
		InitializeComponent();
		MainPage = new NavigationPage(new MainPage())
		{
			BarBackgroundColor = Color.FromArgb("#FF02653F"),
			BarTextColor = Colors.White
		};
	}
}
