using SQLite;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CustomerType { get; set; }  // "Corporate" or "General"
}