using lab4.DTO;
using lab4.Service.Base;
using lab4.Commands.Base;

namespace lab4.Commands
{
    public class PlayGameCommand : ICommand
    {
        private IGameService _gameService;
        private IAccountService _accountService;
        private GameFactory _factory;

        public PlayGameCommand(IGameService gameService, IAccountService accountService, GameFactory factory)
        {
            _gameService = gameService;
            _accountService = accountService;
            _factory = factory;
        }

        public string GetInfo()
        {
            return "Play a game";
        }

        public void Action()
        {
            Console.WriteLine("Available players:");
            foreach (var p in _accountService.GetAll())
            {
                Console.WriteLine($"ID: {p.Id} | Name: {p.UserName} | Rating: {p.CurrentRating}");
            }
            Console.WriteLine();

            Console.WriteLine("Enter ID of the first player:");

            int p1Id = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter ID of the second player:");
            int p2Id = int.Parse(Console.ReadLine()!);

            var p1 = _accountService.GetById(p1Id);
            var p2 = _accountService.GetById(p2Id);
            if (p1 == null || p2 == null) 
            {
                Console.WriteLine("Player not found!"); 
                return; 
            }
            if (p1Id == p2Id) 
            {
                Console.WriteLine("Player cannot play against themselves!"); 
                return; 
            }

            Console.WriteLine("Enter game type (standard / training / ranked):");
            string type = Console.ReadLine()!;

            Console.WriteLine("Enter game rating (number):");
            int rating = int.Parse(Console.ReadLine()!);

            var game = _factory.CreateGame(type, rating);

            Console.WriteLine("Who won? (1 - first player, 2 - second):");
            int winner = int.Parse(Console.ReadLine()!);
            bool isP1Winner = (winner == 1);

            _gameService.Create(p1Id, p2Id, game, isP1Winner);
            Console.WriteLine("The game was successfully conducted!");
        }
    }

}