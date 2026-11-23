using lab4.DTO;

namespace lab4.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private DbContext db;

        public AccountRepository(DbContext db)
        {
            this.db = db;
        }

        public List<GameAccount> GetAll()
        {
            return db.Player.ToList();
        }

        public GameAccount? GetById(int id)
        {
            return db.Player.FirstOrDefault(x => x.Id == id);
        }

        public void Add(GameAccount gameAccount)
        {
            gameAccount.Id = db.Player.Count > 0 ? db.Player.Max(x => x.Id) + 1 : 1;
            db.Player.Add(gameAccount);
        }

        public void Delete(int id)
        {
            var gameAccount = db.Player.FirstOrDefault(x => x.Id == id);
            if (gameAccount != null)
            {
                db.Player.Remove(gameAccount);
            }
        }

        public void Update(GameAccount gameAccount)
        {
            var existing = db.Player.FirstOrDefault(x => x.Id == gameAccount.Id);
            if (existing == null) return;

            existing.UserName = gameAccount.UserName;
            existing.SetRating(gameAccount.CurrentRating);
        }
    }
}