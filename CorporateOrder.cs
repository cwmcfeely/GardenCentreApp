using SQLite;
/// Represents a corporate order in the system
public class CorporateOrder
{
    /// This is automatically generated as an auto-incrementing primary key
    [PrimaryKey, AutoIncrement]
    public int CorpOrderID { get; set; }
    /// This field links to the corp user's record in the system.
    public int UserID { get; set; }
    /// Stored as a decimal to ensure precise financial calculations
    public decimal Amount { get; set; }
    /// Gets or sets the date and time when the order was made
    public DateTime PurchaseDate { get; set; }
    /// True if the order is settled, false if payment is still pending
    public bool IsSettled { get; set; }
}
