namespace lab5.DTO
{
    public class DbContext
    {
      public List<SteamAccount> Accounts { get; set; }
      public List<StoreGame> StoreGames { get; set; }
      public List<Purchase> Purchases { get; set; }

        public DbContext()
        {
            Accounts = new List<SteamAccount>();
            StoreGames = new List<StoreGame>();
            Purchases = new List<Purchase>();
        }

        public void FillDB()
        {
            Accounts.Add(new SteamAccount("SoulsJ", "!364", 1000) { Id = 1 });
            Accounts.Add(new SteamAccount("HelpfulGamer", "1234", 500) { Id = 2 });
            StoreGames.Add(new StandardGame("Resident Evil Requim", 1999.00m, 60) { Id = 1 });
            StoreGames.Add(new DeluxeGame("Resident Evil Requim. Deluxe Edition", 2000.00m, 30) { Id = 2 });
            StoreGames.Add(new DiscountGame("Silent Hill 2", 1200.00m, 70) { Id = 3 });
        }
    }
}