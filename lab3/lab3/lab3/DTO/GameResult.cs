namespace lab3.DTO
{
    public class GameResult
    {
        public int GameID { get; set; }
        public int PlayerId { get; set; }
        public int Rating { get; set; }
        public string OpponentName { get; set; }
        public bool IsWin { get; set; }
        public string GameType { get; set; }

        public GameResult(string opponent, int rating, int playerId, bool isWin, string gameType)
        {
            PlayerId = playerId;
            OpponentName = opponent;
            Rating = rating;
            IsWin = isWin;
            GameType = gameType;
        }
    }
}