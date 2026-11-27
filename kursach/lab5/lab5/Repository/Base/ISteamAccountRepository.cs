using lab5.DTO;

namespace lab5.Repository
{
    public interface ISteamAccountRepository
    {
        public List<SteamAccount> GetAll();
        public SteamAccount? GetById(int id);
        public SteamAccount? GetByUserName(string userName);
        public void Add(SteamAccount account);
        public void Delete(int id);
        public void Update(SteamAccount account);

    }
}