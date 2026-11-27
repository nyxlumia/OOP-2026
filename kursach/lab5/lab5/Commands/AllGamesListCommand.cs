using lab5.Commands.Base;
using lab5.Service.Base;
using lab5.Service.Views;

namespace lab5.Commands
{

    public class AllGamesListCommand : ICommand
    {
        private IStoreService _storeService;

        public AllGamesListCommand(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public string GetInfo()
        {
            return "View all games";
        }

        public void Action()
        {
            var games = _storeService.GetAllGames();
            foreach (var game in games)
            {
                var view = new StoreGameView
                {
                    GameID = game.Id,
                    GameTitle = game.Title,
                    GameType = game.GetType().Name,
                    KeysCount = game.KeysCount,
                    Price = game.GetPrice()
                };
                Console.WriteLine(view);
            }
        }
    }
}