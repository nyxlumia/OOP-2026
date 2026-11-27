using lab5.DTO;
using lab5.Repository;
using lab5.Service.Base;

namespace lab5.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ISteamAccountRepository _accountRepository;
        private readonly IAuthSteamAccountService _authService;

        public StoreService(
            IStoreRepository storeRepository,
            IPurchaseRepository purchaseRepository,
            ISteamAccountRepository accountRepository,
            IAuthSteamAccountService authService)
        {
            _storeRepository = storeRepository;
            _purchaseRepository = purchaseRepository;
            _accountRepository = accountRepository;
            _authService = authService;
        }

        public List<StoreGame> GetAllGames()
        {
            return _storeRepository.GetAll();
        }

        public List<StoreGame> SearchGamesByTitle(string title)
        {
            return _storeRepository.GetAll()
                .Where(g => g.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void AddGame(StoreGame game)
        {
            _storeRepository.Add(game);
        }

        public StoreGame? GetGameById(int id)
        {
            return _storeRepository.GetById(id);
        }

        public void BuyGame(int gameId)
        {

            var user = _authService.CurrentAccount;
            if (user == null)
            {
                throw new InvalidOperationException("You must log in first.");
            }

            var game = _storeRepository.GetById(gameId);
            if (game == null)
            {
                throw new ArgumentException("Game not found.");
            }

            var alreadyOwned = _purchaseRepository.GetByUserId(user.Id).Any(p => p.GameID == game.Id);
            if (alreadyOwned)
            {
                throw new InvalidOperationException("You already own this game!");
            }

            if (game.KeysCount <= 0)
            {
                throw new InvalidOperationException("No keys left for this game.");
            }

            decimal finalPrice = game.GetPrice();
            user.TopLoseBalance(finalPrice);
            game.KeysCount--;

            _accountRepository.Update(user);
            _storeRepository.Update(game);
            _purchaseRepository.Add(new Purchase(user.Id, game.Id, game.Title, finalPrice));
        }

        public void BuyGame(string title)
        {
            var game = _storeRepository.GetAll()
                .FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (game == null)
            {
                throw new ArgumentException("Game not found.");
            }

            BuyGame(game.Id);
        }

        public List<Purchase> GetUserPurchases()
        {
            var user = _authService.CurrentAccount;
            if (user == null)
            {
                throw new InvalidOperationException("You must log in first.");
            }

            return _purchaseRepository.GetByUserId(user.Id);
        }
    }
}