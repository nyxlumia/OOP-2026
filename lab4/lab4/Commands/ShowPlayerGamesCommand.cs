using lab4.Commands.Base;
using lab4.Service.Base;
using Lab4.Service.Views;

namespace lab4.Commands
{

    public class ShowPlayerGamesCommand : ICommand
    {
        private IGameService _gameService;

        public ShowPlayerGamesCommand(IGameService gameService)
        {
            _gameService = gameService;
        }

        public string GetInfo()
        {
            return "View all players";
        }

        public void Action()
        {
            Console.WriteLine("Enter the player ID to see their games:");
            if (!int.TryParse(Console.ReadLine(), out int playerId))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var games = _gameService.ReadPlayerGamesByPlayerId(playerId);

            if (games.Count == 0)
            {
                Console.WriteLine("This player has not played any games yet.");
                return;
            }

            Console.WriteLine($" Games of the player with ID {playerId} ");
            foreach (var game in games)
            {
                var view = new GameResultView
                {
                    GameID = game.GameID,
                    OpponentName = game.OpponentName,
                    GameType = game.GameType,
                    Rating = game.Rating,
                    Result = game.IsWin ? "Victory" : "Defeat"
                };
                Console.WriteLine(view);
            }
        }
    }
}