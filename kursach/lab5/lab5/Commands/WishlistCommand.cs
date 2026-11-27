using lab5.Commands.Base;
using lab5.Service.Base;

namespace lab5.Commands;

public class WishlistCommand : ICommand
{
    private readonly IAuthSteamAccountService _accountService;
    private readonly IStoreService _storeService;
    public WishlistCommand(IAuthSteamAccountService accountService, IStoreService storeService)
    {
        _accountService = accountService;
        _storeService = storeService;
    }

    public string GetInfo()
    {
        return "View wishlist";
    }

    public void Action()
    {
        var user = _accountService.CurrentAccount;
        if (user == null)
        {
            Console.WriteLine("Error: you must log in first.");
            return;
        }

        Console.WriteLine("Wishlist");
        if (user.WishlistGameIds.Count == 0)
        {
            Console.WriteLine("Wishlist is empty.");
        }
        else
        {
            foreach (var id in user.WishlistGameIds)
            {
                var wishGame = _storeService.GetGameById(id);
                if (wishGame != null)
                {
                    Console.WriteLine($"ID: {wishGame.Id} | Title: {wishGame.Title} | Price: {wishGame.GetPrice()} UAH");
                }
            }
        }

        Console.WriteLine("\nEnter a game ID or Title to add to wishlist (or press Enter to skip):");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return;
        }

        DTO.StoreGame? game;
        if (int.TryParse(input, out int gameId))
        {
            game = _storeService.GetGameById(gameId);
        }
        else
        {
            game = _storeService.SearchGamesByTitle(input).FirstOrDefault();
        }

        if (game == null)
        {
            Console.WriteLine("Game not found.");
            return;
        }

        _accountService.AddToWishlist(game.Id);
        Console.WriteLine($"Added to wishlist: {game.Title}, Price: {game.GetPrice()} UAH");
    }
}