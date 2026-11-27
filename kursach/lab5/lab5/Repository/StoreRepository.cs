using lab5.DTO;

namespace lab5.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private DbContext db;

        public StoreRepository(DbContext db)
        {
            this.db = db;
        }

        public List<StoreGame> GetAll()
        {
            return db.StoreGames.ToList();
        }

        public StoreGame? GetById(int id)
        {
            return db.StoreGames.FirstOrDefault(x => x.Id == id);
        }

        public StoreGame? GetByTitle(string title)
        {
            return db.StoreGames.FirstOrDefault(x => x.Title == title);
        }

        public void Add(StoreGame storeGame)
        {
            storeGame.Id = db.StoreGames.Count > 0 ? db.StoreGames.Max(x => x.Id) + 1 : 1;
            db.StoreGames.Add(storeGame);
        }

        public void Delete(int id)
        {
            var storeGame = db.StoreGames.FirstOrDefault(x => x.Id == id);
            if (storeGame != null)
            {
                db.StoreGames.Remove(storeGame);
            }
        }

        public void Update(StoreGame storeGame)
        {
            var existing = db.StoreGames.FirstOrDefault(x => x.Id == storeGame.Id);
            if (existing == null) return;

            existing.Title = storeGame.Title;
            existing.KeysCount = storeGame.KeysCount;
        }
    }
}