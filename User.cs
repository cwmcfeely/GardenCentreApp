using SQLite;

/// Represents a user in the garden centre application
public class User
{
    /// Acts as the primary key in the SQLite database
    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }
    /// The user's display name or full name
    public string UserName { get; set; }
    /// The user's email address
    public string Email { get; set; }
    /// Used for account verification and communication
    public string PhoneNumber { get; set; }
    /// Indicates whether the user is a corporate or general customer
    public string CustomerType { get; set; }
}
