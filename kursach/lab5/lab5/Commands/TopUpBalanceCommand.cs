using lab5.Commands.Base;
using lab5.Service.Base;

namespace lab5.Commands;

public class TopUpBalanceCommand : ICommand
{
    private readonly IAuthSteamAccountService _accountService;

    public TopUpBalanceCommand(IAuthSteamAccountService accountService)
    {
        _accountService = accountService;
    }

    public string GetInfo()
    {
        return "Top up wallet balance";
    }

    public void Action()
    {
        Console.WriteLine("Enter amount to top up:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Error: please enter a valid positive number.");
            return;
        }

        try
        {
            _accountService.TopUpBalance(amount);
            Console.WriteLine($"Balance topped up! New balance: {_accountService.CurrentAccount?.Balance} UAH");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Top-up failed: {ex.Message}");
        }
    }
}