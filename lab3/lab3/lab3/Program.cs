using System;
using System.Collections.Generic;
using lab3.DTO;
using lab3.Repository;
using lab3.Service.Base;
using lab3.Service;

class Program
{
    static void SimulateMatches(int p1Id, int p2Id, IGameService gameService, GameFactory factory, Random rng, int gamesCount)
    {
        string[] gameTypes = { "standard", "training", "ranked" };

        for (int i = 0; i < gamesCount; i++)
        {
            int randomRating = rng.Next(1, 10);
            string type = gameTypes[rng.Next(gameTypes.Length)];
            BaseGame game = factory.CreateGame(type, randomRating);
            bool isPlayer1Winner = rng.Next(0, 2) == 0;
            gameService.Create(p1Id, p2Id, game, isPlayer1Winner);
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        GameFactory factory = new GameFactory();
        Random rng = new Random();

        DbContext db = new DbContext();
        IAccountRepository accountRepository = new AccountRepository(db);
        IGameRepository gameRepository = new GameRepository(db);
        IAccountService accountService = new AccountService(accountRepository);
        IGameService gameService = new GameService(gameRepository, accountRepository);

        Console.WriteLine("Створення нових гравців");
        accountService.Create(new BeginnerGameAccount("Новачок"));
        accountService.Create(new ProGameAccount("Про-гравець"));
        Console.WriteLine("Створено 2 нових гравців.\n");

        Console.WriteLine("Список усіх гравців");
        foreach (var p in accountService.GetAll())
        {
            Console.WriteLine($"Id: {p.Id}, Name: {p.UserName}, Type: {p.GetType().Name}, Rating: {p.CurrentRating}");
        }
        Console.WriteLine();

        SimulateMatches(1, 2, gameService, factory, rng, 5);
        SimulateMatches(3, 4, gameService, factory, rng, 5);
        SimulateMatches(5, 6, gameService, factory, rng, 5);

        foreach (var player in accountService.GetAll())
        {
            Console.WriteLine($"Player: {player.UserName}, Id: {player.Id}, Type: {player.GetType().Name}, Rating: {player.CurrentRating}");
            var results = gameService.ReadPlayerGamesByPlayerId(player.Id);
            foreach (var result in results)
            {
                Console.WriteLine($"Opponent: {result.OpponentName}, Rating: {result.Rating}, Type: {result.GameType}, IsWinner: {result.IsWin}");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Всі проведені ігри");
        foreach (var g in gameService.GetAll())
        {
            Console.WriteLine($"GameID: {g.GameID}, PlayerId: {g.PlayerId}, Opponent: {g.OpponentName}, Rating: {g.Rating}, Type: {g.GameType}, Win: {g.IsWin}");
        }
        Console.WriteLine();
    }
}