using lab5.DTO;

namespace lab5.Repository
{

    public interface IStoreRepository
    {
        public List<StoreGame> GetAll();
        public StoreGame? GetById(int id);
        public StoreGame? GetByTitle(string title);
        public void Add(StoreGame game);
        public void Delete(int id);
        public void Update(StoreGame game);
    }
}