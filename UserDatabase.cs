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
            // Adding corporate order table
            _database.CreateTableAsync<CorporateOrder>().Wait();
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

        /// Calculates the total amount of all corporate orders for a specific user within the current month
        public async Task<decimal> GetMonthlyTotalAsync(int userID)
        {
            // Calculate the first day of the current month (e.g., 2024-01-01 00:00:00)
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Calculate the last day of the current month (e.g., 2024-01-31 23:59:59)
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            // Query the database asynchronously to retrieve all orders that match:
            var monthlyOrders = await _database.Table<CorporateOrder>()
                                               .Where(o => o.UserID == userID &&
                                                           o.PurchaseDate >= startOfMonth &&
                                                           o.PurchaseDate <= endOfMonth)
                                               .ToListAsync();

            // Calculate and return the sum of all order amounts
            // If no orders exist, returns 0
            return monthlyOrders.Sum(o => o.Amount);
        }

        /// Saves a new corporate order to the database
        public async Task SaveCorporateOrderAsync(CorporateOrder order)
        {
            // Insert the new order into the database
            await _database.InsertAsync(order);
        }

        /// Retrieves a specific user from the database based on their ID
        public async Task<User> GetUserByIdAsync(int userID)
        {
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.UserID == userID);
        }
    }
}
