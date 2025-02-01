using SQLite;

namespace GardenCentreApp
{

    /// Handles all database operations related to users
    public class UserDatabase
    {

        /// Connection to the SQLite database
        /// Handles asynchronous database operations
        private SQLiteAsyncConnection _database;

        /// Initializes a new instance of the UserDatabase
        /// Creates the User table if it doesn't exist
        public UserDatabase(string dbPath)
        {
            // Create new database connection
            _database = new SQLiteAsyncConnection(dbPath);
            // Create User table synchronously to ensure it exists before any operations
            _database.CreateTableAsync<User>().Wait();
        }

        /// Retrieves all users from the database
        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        /// Retrieves a specific user from the database based on email and phone number
        /// Used for user authentication during login
        public async Task<User> GetUserAsync(string email, string phoneNumber)
        {
            return await _database.Table<User>()
                .Where(u => u.Email == email && u.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
        }

        /// Saves a new user to the database
        public async Task<int> SaveUserAsync(User user)
        {
            return await _database.InsertAsync(user);
        }
    }
}
