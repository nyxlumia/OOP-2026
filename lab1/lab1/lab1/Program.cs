using System;
using System.Collections.Generic;

public class GameAccount
{
    public string UserName { get; set; }

    private int _currentRating;
    public int CurrentRating
    {
        get { return _currentRating; }
        private set { _currentRating = value < 1 ? 1 : value; }
    }

    private List<Game> _allGames = new List<Game>();
    public int GamesCount => _allGames.Count;

    public GameAccount(string userName, int startRating = 100)
    {
        UserName = userName;
        CurrentRating = startRating;
    }

    private void ValidateRating(int rating)
    {
        if (rating < 0)
            throw new ArgumentException("Rating is negative");
    }

    public void WinGame(string opponent, int rating)
    {
        ValidateRating(rating);
        CurrentRating += rating;
        _allGames.Add(new Game(opponent, rating, true));
    }

    public void LoseGame(string opponent, int rating)
    {
        ValidateRating(rating);
        CurrentRating -= rating;
        _allGames.Add(new Game(opponent, rating, false));
    }

    public void ShowHistory()
    {
        foreach (Game game in _allGames)
        {
            Console.WriteLine(
                $"Гра №{game.GameID} проти {game.OpponentName} | " +
                $"Рейтинг: {game.Rating} | " +
                $"Результат: {(game.IsWin ? "Перемога" : "Поразка")}");
        }
    }
}

public class Game
{
    private static int _idCounter = 1;
    public int GameID { get; }
    public int Rating { get; }
    public string OpponentName { get; }
    public bool IsWin { get; }

    public Game(string opponent, int rating, bool isWin)
    {
        OpponentName = opponent;
        Rating = rating;
        IsWin = isWin;
        GameID = _idCounter++;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        GameAccount player1 = new GameAccount("Свят");
        GameAccount player2 = new GameAccount("Давид");

        Random rng = new Random();
        for (int i = 0; i < 5; i++)
        {
            int randomRating = rng.Next(10, 30);
            int winner = rng.Next(0, 2);

            if (winner == 0)
            {
                player1.WinGame(player2.UserName, randomRating);
                player2.LoseGame(player1.UserName, randomRating);
            }
            else
            {
                player2.WinGame(player1.UserName, randomRating);
                player1.LoseGame(player2.UserName, randomRating);
            }
        }

        Console.WriteLine($"--- Історія гравця {player1.UserName} ---");
        player1.ShowHistory();
        Console.WriteLine($"--- Історія гравця {player2.UserName} ---");
        player2.ShowHistory();

        Console.WriteLine($"Фінальний рейтинг гравця {player1.UserName}: {player1.CurrentRating} (ігор: {player1.GamesCount})");
        Console.WriteLine($"Фінальний рейтинг гравця {player2.UserName}: {player2.CurrentRating} (ігор: {player2.GamesCount})");
    }
}