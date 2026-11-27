namespace lab5.Service.Views;

public class PurchaseView
{
    public int UserID { get; set; }
    public string? UserName { get; set; }
    public int PurchaseId { get; set; }
    public int GameID { get; set; }
    public string? GameTitle { get; set; }

    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    public override string ToString()
    {
        return $"UserID: {UserID}, UserName: {UserName}, Purchase ID: {PurchaseId}, Game ID: {GameID}, Game Title: {GameTitle}, Purchase Date: {PurchaseDate}, Price: {Price}";
    }
}