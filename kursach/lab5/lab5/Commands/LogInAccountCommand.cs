using lab5.Commands.Base;
using lab5.Service.Base;

namespace lab5.Commands;

public class LogInAccountCommand : ICommand
{
    private IAuthSteamAccountService _accountService;

    public LogInAccountCommand(IAuthSteamAccountService accountService)
    {
        _accountService = accountService;
    }

    public string GetInfo()
    {
        return "Log in to existing account";
    }

    public void Action()
    {
        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Error: name cannot be empty.");
            return;
        }

        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Error: password cannot be empty.");
            return;
        }

        try
        {
            _accountService.LogIn(username, password);
            Console.WriteLine($"Welcome back, {username}! Current balance: {_accountService.CurrentAccount?.Balance} UAH");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
        }
    }
}