using lab5.Commands.Base;
using lab5.Service.Base;

namespace lab5.Commands
{
    public class BuyGameCommand : ICommand
    {
        private readonly IStoreService _storeService;
        public BuyGameCommand(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public string GetInfo()
        {
            return "Buy a game";
        }

        public void Action()
        {
            Console.WriteLine("Available games:");
            foreach (var game in _storeService.GetAllGames())
            {
                Console.WriteLine($"ID: {game.Id} | Title: {game.Title} | Keys: {game.KeysCount} | Price: {game.GetPrice()}");
            }
            Console.WriteLine();

            Console.Write("Enter game ID or exact Title to buy: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Error: input cannot be empty.");
                return;
            }

            try
            {
                if (int.TryParse(input, out int gameId))
                {
                    _storeService.BuyGame(gameId);
                }
                else
                {
                    _storeService.BuyGame(input);
                }

                Console.WriteLine("Game successfully purchased!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Purchase failed: {ex.Message}");
            }
        }
    }
}
