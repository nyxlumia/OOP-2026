namespace lab3.DTO
{
    public class DbContext
    {
        public List<GameAccount> Player { get; set; }
        public List<GameResult> GameResults { get; set; }

        public DbContext()
        {
            Player = new List<GameAccount>();
            GameResults = new List<GameResult>();

            FillDB();
        }

        private void FillDB()
        {
            GameResults.Add(new GameResult("Opponent 1", 100, 1, true, "RankedGame") { GameID = 1 });
            GameResults.Add(new GameResult("Opponent 2", 0, 1, false, "TrainingGame") { GameID = 2 });

            Player.Add(new BeginnerGameAccount("Свят") { Id = 1 });
            Player.Add(new BeginnerGameAccount("Давид") { Id = 2 });

            Player.Add(new AverageGameAccount("Ліля") { Id = 3 });
            Player.Add(new AverageGameAccount("Роман") { Id = 4 });

            Player.Add(new ProGameAccount("Анна") { Id = 5 });
            Player.Add(new ProGameAccount("Ріта") { Id = 6 });
        }
    }
}