using lab4.DTO;
using lab4.Repository;
using lab4.Service;
using lab4.Service.Base;
using lab4.Commands;
using lab4.Commands.Base;

Console.OutputEncoding = System.Text.Encoding.UTF8;

DbContext db = new DbContext();
IAccountRepository accountRepository = new AccountRepository(db);
IGameRepository gameRepository = new GameRepository(db);

IAccountService accountService = new AccountService(accountRepository);
IGameService gameService = new GameService(gameRepository, accountRepository);
GameFactory factory = new GameFactory();

List<ICommand> commands = new List<ICommand>
{
     new ShowPlayersCommand(accountService),
    new AddPlayerCommand(accountService),
    new ShowAllGamesCommand(gameService),
    
    new PlayGameCommand(gameService, accountService, factory),
    new ShowPlayerGamesCommand(gameService),
    new ShowPlayerStatsCommand(accountService, gameService),

    new ExitCommand()
};


while (true)
{
    Console.WriteLine("\n Main Menu");
    for (int i = 0; i < commands.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {commands[i].GetInfo()}");
    }

    Console.Write("Choose an action: ");
    int choice = int.Parse(Console.ReadLine()!);

    if (choice > 0 && choice <= commands.Count)
    {
        try
        {
            Console.WriteLine();
            commands[choice - 1].Action();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}