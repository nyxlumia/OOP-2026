using lab5.Commands.Base;
using lab5.Service.Base;
using lab5.DTO;

namespace lab5.Commands
{
    public class AddGameCommand : ICommand
    {
        private readonly IStoreService _storeService;

        public AddGameCommand(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public string GetInfo()
        {
            return "Add a new game to store";
        }

        public void Action()
        {
            Console.Write("Enter game title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter base price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Error: invalid price.");
                return;
            }

            Console.Write("Enter keys count: ");
            if (!int.TryParse(Console.ReadLine(), out int keys))
            {
                Console.WriteLine("Error: invalid keys count.");
                return;
            }

            Console.Write("Enter game type (1 - Standard, 2 - Discount, 3 - Deluxe): ");
            string type = Console.ReadLine()!;

            try
            {
                StoreGame game = type switch
                {
                    "2" => new DiscountGame(title, price, keys),
                    "3" => new DeluxeGame(title, price, keys),
                    _ => new StandardGame(title, price, keys)
                };

                _storeService.AddGame(game);
                Console.WriteLine($"Game '{title}' added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add game: {ex.Message}");
            }
        }
    }
}
