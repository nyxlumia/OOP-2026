using lab5.Commands.Base;
using lab5.Service.Base;

namespace lab5.Commands;

public class SignUpAccountCommand : ICommand
{
    private IAuthSteamAccountService _accountService;

    public SignUpAccountCommand(IAuthSteamAccountService accountService)
    {
        _accountService = accountService;
    }

    public string GetInfo()
    {
        return "Sign up for a new account";
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
            _accountService.SignUp(username, password);
            Console.WriteLine($"Greetings, {username}! Current balance: {_accountService.CurrentAccount?.Balance} UAH");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Sign up failed: {ex.Message}");
        }
    }
}