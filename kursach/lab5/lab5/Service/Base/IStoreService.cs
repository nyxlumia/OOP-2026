using lab5.DTO;

namespace lab5.Service.Base
{
    public interface IStoreService
    {
        List<StoreGame> GetAllGames();
        List<StoreGame> SearchGamesByTitle(string title);
        public void AddGame(StoreGame game);
        StoreGame? GetGameById(int id);
        void BuyGame(int gameId);
        void BuyGame(string title);
        List<Purchase> GetUserPurchases();

    }
}