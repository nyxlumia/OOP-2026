using lab5.DTO;
using lab5.Repository;
using lab5.Service.Base;

namespace lab5.Service
{
    public class AuthSteamAccountService : IAuthSteamAccountService
    {
        private readonly ISteamAccountRepository _accountSteamRepository;
        public SteamAccount? CurrentAccount { get; private set; }
        public AuthSteamAccountService(ISteamAccountRepository accountRepository)
        {
            _accountSteamRepository = accountRepository;
        }

        public void SignUp(string userName, string password)
        {
            if (_accountSteamRepository.GetByUserName(userName) != null)
            {
                throw new InvalidOperationException("User with this username already exists.");
            }

            var newAccount = new SteamAccount(userName, password);
            _accountSteamRepository.Add(newAccount);
            CurrentAccount = newAccount;
        }

        public void LogIn(string userName, string password)
        {
            var account = _accountSteamRepository.GetByUserName(userName);
            if (account == null || account.Password != password)
            {
                throw new InvalidOperationException("Invalid username or password.");
            }

            CurrentAccount = account;
        }

        public void TopUpBalance(decimal amount)
        {
            if (CurrentAccount == null)
            {
                throw new InvalidOperationException("You must log in first.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive.");
            }

            CurrentAccount.TopUpBalance(amount);
            _accountSteamRepository.Update(CurrentAccount);
        }

        public void AddToWishlist(int gameId)
        {
            if (CurrentAccount == null)
            {
                throw new InvalidOperationException("You must log in first.");
            }

            if (!CurrentAccount.WishlistGameIds.Contains(gameId))
            {
                CurrentAccount.WishlistGameIds.Add(gameId);
                _accountSteamRepository.Update(CurrentAccount);
            }
        }
    }
}