using lab5.DTO;

namespace lab5.Repository
{

    public interface IPurchaseRepository
    {
        public List<Purchase> GetAll();
        public Purchase? GetById(int id);
        public List<Purchase> GetByUserId(int userId);
        public void Add(Purchase purchase);
        public void Delete(int id);
        public void Update(Purchase purchase);

    }
}