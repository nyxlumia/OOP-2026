using lab4.Commands.Base;
using lab4.Service.Base;
using Lab4.Service.Views;
using System;

namespace lab4.Commands
{
    public class ShowPlayerStatsCommand : ICommand
    {
        private IAccountService _accountService;
        private IGameService _gameService;

        public ShowPlayerStatsCommand(IAccountService accountService, IGameService gameService)
        {
            _accountService = accountService;
            _gameService = gameService;
        }

        public string GetInfo()
        {
            return "Show player statistics";
        }

        public void Action()
        {
            Console.WriteLine("Enter the player ID:");

            if (!int.TryParse(Console.ReadLine(), out int playerId))
            {
                Console.WriteLine("Error: invalid ID.");
                return;
            }

            var player = _accountService.GetById(playerId);
            if (player == null)
            {
                Console.WriteLine("Player with such ID not found.");
                return;
            }

            // 1. Виводимо інформацію про гравця
            Console.WriteLine("\n Стата гравця");
            var playerView = new PlayerView
            {
                Id = player.Id,
                UserName = player.UserName,
                AccountType = player.GetType().Name,
                Rating = player.CurrentRating
            };
            Console.WriteLine(playerView);

            // 2. Виводимо його ігри
            var games = _gameService.ReadPlayerGamesByPlayerId(playerId);

            if (games.Count == 0)
            {
                Console.WriteLine("This player has not played any games yet.");
                return;
            }

            Console.WriteLine("\n Games of the player");
            foreach (var game in games)
            {
                var gameView = new GameResultView
                {
                    GameID = game.GameID,
                    OpponentName = game.OpponentName,
                    GameType = game.GameType,
                    Rating = game.Rating,
                    Result = game.IsWin ? "Victory" : "Defeat"
                };
                Console.WriteLine(gameView);
            }
            Console.WriteLine();
        }
    }
}