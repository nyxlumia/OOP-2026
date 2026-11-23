using lab4.Commands.Base;
using lab4.Service.Base;
using Lab4.Service.Views;

namespace lab4.Commands;

public class ShowAllGamesCommand : ICommand
{
    IGameService gameService;

    public ShowAllGamesCommand(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public string GetInfo()
    {
        return "View all games";
    }

    public void Action()
    {
        var games = gameService.GetAll();

        if (games == null || games.Count == 0)
        {
            Console.WriteLine("The games have not yet been held.");
            return;
        }

        foreach (var item in games)
        {
            var view = new GameResultView
            {
                GameID = item.GameID,
                OpponentName = item.OpponentName,
                GameType = item.GameType,
                Rating = item.Rating,
                Result = item.IsWin ? "Victory" : "Defeat"
            };

            Console.WriteLine(view);
        }
    }
}