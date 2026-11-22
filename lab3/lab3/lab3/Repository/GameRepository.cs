using lab3.DTO;

namespace lab3.Repository
{
    public class GameRepository : IGameRepository
    {
        private DbContext db;

        public GameRepository(DbContext db)
        {
            this.db = db;
        }

        public List<GameResult> GetAll()
        {
            return db.GameResults.ToList();
        }

        public GameResult? GetById(int id)
        {
            return db.GameResults.FirstOrDefault(x => x.GameID == id);
        }

        public void Add(GameResult game)
        {
            if (game.GameID == 0)
            {
                game.GameID = db.GameResults.Count > 0
                    ? db.GameResults.Max(x => x.GameID) + 1
                    : 1;
            }
            db.GameResults.Add(game);
        }

        public void Delete(int id)
        {
            var game = db.GameResults.FirstOrDefault(x => x.GameID == id);
            if (game != null)
            {
                db.GameResults.Remove(game);
            }
        }

        public void Update(GameResult game)
        {
            var existing = db.GameResults.FirstOrDefault(x => x.GameID == game.GameID);
            if (existing == null) return;

            existing.PlayerId = game.PlayerId;
            existing.OpponentName = game.OpponentName;
            existing.Rating = game.Rating;
            existing.IsWin = game.IsWin;
            existing.GameType = game.GameType;
        }
    }
}