using lab5.DTO;

namespace lab5.Repository
{
    public class SteamAccountRepository : ISteamAccountRepository
    {
        private DbContext db;

        public SteamAccountRepository(DbContext db)
        {
            this.db = db;
        }

        public List<SteamAccount> GetAll()
        {
            return db.Accounts.ToList();
        }

        public SteamAccount? GetById(int id)
        {
            return db.Accounts.FirstOrDefault(x => x.Id == id);
        }

        public SteamAccount? GetByUserName(string userName)
        {
            return db.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public void Add(SteamAccount steamAccount)
        {
            steamAccount.Id = db.Accounts.Count > 0 ? db.Accounts.Max(x => x.Id) + 1 : 1;
            db.Accounts.Add(steamAccount);
        }

        public void Delete(int id)
        {
            var steamAccount = db.Accounts.FirstOrDefault(x => x.Id == id);
            if (steamAccount != null)
            {
                db.Accounts.Remove(steamAccount);
            }
        }

        public void Update(SteamAccount steamAccount)
        {
            var existing = db.Accounts.FirstOrDefault(x => x.Id == steamAccount.Id);
            if (existing == null) return;

            existing.UserName = steamAccount.UserName;
            existing.SetBalance(steamAccount.Balance);
            existing.WishlistGameIds.Clear();
            existing.WishlistGameIds.AddRange(steamAccount.WishlistGameIds);
        }
    }
}