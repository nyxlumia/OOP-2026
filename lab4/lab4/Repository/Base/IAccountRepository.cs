using lab4.DTO;

namespace lab4.Repository
{
    public interface IAccountRepository
    {
        public List<GameAccount> GetAll();
        public GameAccount? GetById(int id);
        public void Add(GameAccount account);
        public void Delete(int id);
        public void Update(GameAccount account);

    }
}