using lab4.DTO;
using lab4.Commands.Base;
using lab4.Service.Base;

namespace lab4.Commands;

public class AddPlayerCommand : ICommand
{
    private IAccountService _accountService;

    public AddPlayerCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string GetInfo()
    {
        return "Add new player";
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

        Console.WriteLine("Enter account type (beginner, average, pro):");
        var type = Console.ReadLine()?.ToLower();

        GameAccount? newPlayer = null;

        if (type == "beginner")
        {
            newPlayer = new BeginnerGameAccount(username);
        }
        else if (type == "average")
        {
            newPlayer = new AverageGameAccount(username);
        }
        else if (type == "pro")
        {
            newPlayer = new ProGameAccount(username);
        }
        else
        {
            Console.WriteLine("Error: unknown account type.");
            return;
        }

        _accountService.Create(newPlayer);
        Console.WriteLine($"Player {username} added successfully!");
    }
}