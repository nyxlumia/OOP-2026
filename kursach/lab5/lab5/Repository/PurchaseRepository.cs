using lab5.DTO;

namespace lab5.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private DbContext db;

        public PurchaseRepository(DbContext db)
        {
            this.db = db;
        }

        public List<Purchase> GetAll()
        {
            return db.Purchases.ToList();
        }

        public Purchase? GetById(int id)
        {
            return db.Purchases.FirstOrDefault(x => x.Id == id);
        }
        public List<Purchase> GetByUserId(int userId)
        {
            return db.Purchases.Where(x => x.UserID == userId).ToList();
        }
        public void Add(Purchase purchase)
        {
            purchase.Id = db.Purchases.Count > 0 ? db.Purchases.Max(x => x.Id) + 1 : 1;
            db.Purchases.Add(purchase);
        }

        public void Delete(int id)
        {
            var purchase = db.Purchases.FirstOrDefault(x => x.Id == id);
            if (purchase != null)
            {
                db.Purchases.Remove(purchase);
            }
        }

        public void Update(Purchase purchase)
        {
            var existing = db.Purchases.FirstOrDefault(x => x.Id == purchase.Id);
            if (existing == null) return;

            existing.UserID = purchase.UserID;
            existing.GameID = purchase.GameID;
            existing.PurchaseDate = purchase.PurchaseDate;
            existing.Price = purchase.Price;
            existing.GameTitle = purchase.GameTitle;
        }
    }
}