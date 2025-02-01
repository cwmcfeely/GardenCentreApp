using SQLite;

namespace GardenCentreApp
{
    public class UserDatabase
    {
        private SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(string email, string phoneNumber)
        {
            return await _database.Table<User>()
                .Where(u => u.Email == email && u.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            return await _database.InsertAsync(user);
        }
    }
}