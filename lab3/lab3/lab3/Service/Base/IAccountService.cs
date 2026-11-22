using lab3.DTO;

namespace lab3.Service.Base
{
    public interface IAccountService
    {
        public void Create(GameAccount player);
        public GameAccount? GetById(int id);
        public List<GameAccount> GetAll();
        public List<GameAccount> GetStrongestAccounts(int count);
    }
}