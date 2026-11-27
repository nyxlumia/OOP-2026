using lab5.DTO;
using lab5.Commands;
using lab5.Commands.Base;
using lab5.Repository;
using lab5.Service;
using lab5.Service.Base;

Console.OutputEncoding = System.Text.Encoding.UTF8;

DbContext db = new DbContext();
db.FillDB();
ISteamAccountRepository accountRepository = new SteamAccountRepository(db);
IPurchaseRepository purchaseRepository = new PurchaseRepository(db);
IStoreRepository storeRepository = new StoreRepository(db);

IAuthSteamAccountService authService = new AuthSteamAccountService(accountRepository);
IStoreService storeService = new StoreService(storeRepository, purchaseRepository, accountRepository, authService);

List<ICommand> commands = new List<ICommand>
{
    new LogInAccountCommand(authService),
    new SignUpAccountCommand(authService),
    new AllGamesListCommand(storeService),
    new AddGameCommand(storeService),
    new BuyGameCommand(storeService),
    new TopUpBalanceCommand(authService),
    new AccountGameListCommand(storeService, authService),
    new WishlistCommand(authService, storeService),
    new ExitCommand()
};

while (true)
{
    var currentUser = authService.CurrentAccount;
    string status = currentUser != null
        ? $"Logged in as: {currentUser.UserName} (Balance: {currentUser.Balance} UAH)"
        : "Not logged in";

    Console.WriteLine($"\nSteam Store Menu [{status}]");
    for (int i = 0; i < commands.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {commands[i].GetInfo()}");
    }

    Console.Write("Choose an action: ");
    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Please enter a valid number.");
        continue;
    }

    if (choice > 0 && choice <= commands.Count)
    {
        try
        {
            Console.WriteLine();
            commands[choice - 1].Action();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Operation error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Input error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
}