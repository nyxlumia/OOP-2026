using lab4.DTO;
using lab4.Repository;
using lab4.Service.Base;

namespace lab4.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Create(GameAccount player)
        {
            _accountRepository.Add(player);
        }

        public GameAccount? GetById(int id)
        {
            return _accountRepository.GetById(id);
        }

        public List<GameAccount> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public List<GameAccount> GetStrongestAccounts(int count)
        {
            var accounts = _accountRepository.GetAll();
            return accounts.OrderByDescending(a => a.CurrentRating).Take(count).ToList();
        }
    }
}