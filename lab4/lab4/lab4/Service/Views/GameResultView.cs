namespace Lab4.Service.Views;

public class GameResultView
{
    public int GameID { get; set; }
    public string OpponentName { get; set; }
    public string GameType { get; set; }
    public int Rating { get; set; }
    public string Result { get; set; } 

    public override string ToString()
    {
        return $"Game ID: {GameID}, Opponent: {OpponentName}, Type: {GameType}, Rating: {Rating}, Result: {Result}";
    }
}