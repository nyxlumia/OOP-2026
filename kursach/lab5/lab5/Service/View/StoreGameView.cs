namespace lab5.Service.Views;

public class StoreGameView
{
    public int GameID { get; set; }
    public string? GameTitle { get; set; }
    public string? GameType { get; set; }
    public int KeysCount { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"Game ID: {GameID}, Game Title: {GameTitle}, Game Type: {GameType}, Keys Count: {KeysCount}, Price: {Price}"; 
    }
}