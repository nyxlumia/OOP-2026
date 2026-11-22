using System;
using System.Collections.Generic;

public class GameAccount
{
    public string UserName { get; set; }

    private int _currentRating;
    public int CurrentRating
    {
        get { return _currentRating; }
        protected set { _currentRating = value < 1 ? 1 : value; }
    }

    protected List<GameResult> _allGames = new List<GameResult>();
    public int GamesCount => _allGames.Count;

    public GameAccount(string userName, int startRating = 100)
    {
        UserName = userName;
        CurrentRating = startRating;
    }

    public virtual void WinGame(string opponent, BaseGame game)
    {
        CurrentRating += game.GetRating();
        _allGames.Add(new GameResult(opponent, game.GetRating(), true));
    }

    public virtual void LoseGame(string opponent, BaseGame game)
    {
        CurrentRating -= game.GetRating();
        _allGames.Add(new GameResult(opponent, game.GetRating(), false));
    }

    public void GetStats()
    {
        foreach (GameResult game in _allGames)
        {
            Console.WriteLine(
                $"Гра №{game.GameID} проти {game.OpponentName} | " +
                $"Рейтинг: {game.Rating} | " +
                $"Результат: {(game.IsWin ? "Перемога" : "Поразка")}");
        }
    }
}

public class BeginnerGameAccount : GameAccount
{
    public BeginnerGameAccount(string userName, int startRating = 100) : base(userName, startRating) { }
    public override void WinGame(string opponent, BaseGame game)
    {
        if (CurrentRating < 1000)
        {
            CurrentRating += game.GetRating() * 2;
            _allGames.Add(new GameResult(opponent, game.GetRating() * 2, true));
        }
        else
        {
            base.WinGame(opponent, game);
        }
    }
    public override void LoseGame(string opponent, BaseGame game)
    {
        if (CurrentRating < 1000)
        {
            CurrentRating -= game.GetRating() / 2;
            _allGames.Add(new GameResult(opponent, game.GetRating() / 2, false));
        }
        else
        {
            base.LoseGame(opponent, game);
        }
    }
}

public class AverageGameAccount : GameAccount
{
    public AverageGameAccount(string userName, int startRating = 1000) : base(userName, startRating) { }
    public override void WinGame(string opponent, BaseGame game)
    {
        if (CurrentRating >= 1000 && CurrentRating <= 1000000)
        {
            CurrentRating += game.GetRating() * 10;
            _allGames.Add(new GameResult(opponent, game.GetRating() * 10, true));
        }
        else
        {
            base.WinGame(opponent, game);
        }
    }
    public override void LoseGame(string opponent, BaseGame game)
    {
        if (CurrentRating >= 1000 && CurrentRating <= 1000000)
        {
            CurrentRating -= game.GetRating() / 10;
            _allGames.Add(new GameResult(opponent, game.GetRating() / 10, false));
        }
        else
        {
            base.LoseGame(opponent, game);
        }
    }
}

public class ProGameAccount : GameAccount
{
    public ProGameAccount(string userName, int startRating = 1000000) : base(userName, startRating) { }
    public override void WinGame(string opponent, BaseGame game)
    {
        if (CurrentRating >= 1000000)
        {
            CurrentRating += game.GetRating() * 30;
            _allGames.Add(new GameResult(opponent, game.GetRating() * 30, true));
        }
        else
        {
            base.WinGame(opponent, game);
        }
    }
    public override void LoseGame(string opponent, BaseGame game)
    {
        if (CurrentRating >= 1000000)
        {
            CurrentRating -= game.GetRating() / 30;
            _allGames.Add(new GameResult(opponent, game.GetRating() / 30, false));
        }
        else
        {
            base.LoseGame(opponent, game);
        }
    }
}

public abstract class BaseGame
{
    public int BaseRating { get; }
    public abstract int GetRating();
    public BaseGame(int rating)
    {
        if (rating < 1)
        {
            throw new ArgumentException("Рейтинг гри повинен бути більшим за 1.");
        }
        BaseRating = rating;
    }
}

public class TrainingGame : BaseGame
{
    public TrainingGame(int rating) : base(rating) { }
    public override int GetRating()
    {
        return 0;
    }
}
public class StandardGame : BaseGame
{
    public StandardGame(int rating) : base(rating) { }
    public override int GetRating()
    {
        return BaseRating * 20;
    }
}

public class RankedGame : BaseGame
{
    public RankedGame(int rating) : base(rating) { }
    public override int GetRating()
    {
        return BaseRating * 50;
    }
}

public class GameFactory
{
    public BaseGame CreateGame(string type, int rating)
    {
        switch (type)
        {
            case "standard": return new StandardGame(rating);
            case "training": return new TrainingGame(rating);
            case "ranked": return new RankedGame(rating);
            default: throw new ArgumentException($"Невідомий тип гри: {type}");
        }
    }
}

public class GameResult
{
    private static int _idCounter = 1;
    public int GameID { get; }
    public int Rating { get; }
    public string OpponentName { get; }
    public bool IsWin { get; }

    public GameResult(string opponent, int rating, bool isWin)
    {
        OpponentName = opponent;
        Rating = rating;
        IsWin = isWin;
        GameID = _idCounter++;
    }
}

class Program
{
    static void SimulateMatches(GameAccount p1, GameAccount p2, GameFactory factory, Random rng, int gamesCount)
    {
        string[] gameTypes = { "standard", "training", "ranked" };

        for (int i = 0; i < gamesCount; i++)
        {
            int randomRating = rng.Next(1, 10);
            string type = gameTypes[rng.Next(gameTypes.Length)];
            BaseGame game = factory.CreateGame(type, randomRating);

            if (rng.Next(0, 2) == 0)
            {
                p1.WinGame(p2.UserName, game);
                p2.LoseGame(p1.UserName, game);
            }
            else
            {
                p2.WinGame(p1.UserName, game);
                p1.LoseGame(p2.UserName, game);
            }
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        GameAccount player1 = new BeginnerGameAccount("Свят");
        GameAccount player2 = new BeginnerGameAccount("Давид");
        GameAccount player3 = new AverageGameAccount("Ліля");
        GameAccount player4 = new AverageGameAccount("Роман");
        GameAccount player5 = new ProGameAccount("Анна");
        GameAccount player6 = new ProGameAccount("Ріта");

        GameFactory factory = new GameFactory();
        Random rng = new Random();

        SimulateMatches(player1, player2, factory, rng, 6);
        SimulateMatches(player3, player4, factory, rng, 6);
        SimulateMatches(player5, player6, factory, rng, 6);

        Console.WriteLine($"--- Історія гравця {player1.UserName} ---");
        player1.GetStats();
        Console.WriteLine($"--- Історія гравця {player2.UserName} ---");
        player2.GetStats();
        Console.WriteLine($"Фінальний рейтинг гравця {player1.UserName}: {player1.CurrentRating} (ігор: {player1.GamesCount})");
        Console.WriteLine($"Фінальний рейтинг гравця {player2.UserName}: {player2.CurrentRating} (ігор: {player2.GamesCount})");

        Console.WriteLine($"--- Історія гравця {player3.UserName} ---");
        player3.GetStats();
        Console.WriteLine($"--- Історія гравця {player4.UserName} ---");
        player4.GetStats();
        Console.WriteLine($"Фінальний рейтинг гравця {player3.UserName}: {player3.CurrentRating} (ігор: {player3.GamesCount})");
        Console.WriteLine($"Фінальний рейтинг гравця {player4.UserName}: {player4.CurrentRating} (ігор: {player4.GamesCount})");

        Console.WriteLine($"--- Історія гравця {player5.UserName} ---");
        player5.GetStats();
        Console.WriteLine($"--- Історія гравця {player6.UserName} ---");
        player6.GetStats();
        Console.WriteLine($"Фінальний рейтинг гравця {player5.UserName}: {player5.CurrentRating} (ігор: {player5.GamesCount})");
        Console.WriteLine($"Фінальний рейтинг гравця {player6.UserName}: {player6.CurrentRating} (ігор: {player6.GamesCount})");
    }
}