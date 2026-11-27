using lab5.Commands.Base;
using lab5.Service.Base;
using lab5.Service.Views;

namespace lab5.Commands
{
    public class AccountGameListCommand : ICommand
    {
        private readonly IStoreService _storeService;
        private readonly IAuthSteamAccountService _authService;
       public AccountGameListCommand(IStoreService storeService, IAuthSteamAccountService authService)
        {
            _storeService = storeService;
            _authService = authService;
        }
        public string GetInfo()
        {
            return "View my purchase history / library";
        }

        public void Action()
        {
            var purchases = _storeService.GetUserPurchases();

            if (purchases.Count == 0)
            {
                Console.WriteLine("You haven't bought any games yet.");
                return;
            }

            Console.WriteLine($"Purchase history of {_authService.CurrentAccount?.UserName}");
            foreach (var purchase in purchases)
            {
                var view = new PurchaseView
                {
                    UserID = purchase.UserID,
                    UserName = _authService.CurrentAccount?.UserName,
                    PurchaseId = purchase.Id,
                    GameID = purchase.GameID,
                    GameTitle = purchase.GameTitle,
                    Price = purchase.Price,
                    PurchaseDate = purchase.PurchaseDate
                };
                Console.WriteLine(view);
            }
        }
    }
}
