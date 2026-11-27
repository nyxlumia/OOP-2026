using lab5.DTO;

namespace lab5.Service.Base
{
    public interface IAuthSteamAccountService
    {
        SteamAccount? CurrentAccount { get; }
        void SignUp(string userName, string password);
        void LogIn(string userName, string password);
        void TopUpBalance(decimal amount);
        void AddToWishlist(int gameId);
    }
}