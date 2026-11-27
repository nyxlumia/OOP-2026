namespace lab5.DTO
{
    public class Purchase
    {
        public int Id { get; set; }
        public int GameID { get; set; }
        public int UserID { get; set; }
        public decimal Price { get; set; }
        public string GameTitle { get; set; }
        public DateTime PurchaseDate { get; set; } //використала для точного часу покупки. Підглядено.

        public Purchase (int userId, int gameId, string gameTitle, decimal price)
        {
            UserID = userId;
            GameID = gameId;
            GameTitle = gameTitle;
            Price = price;
            PurchaseDate = DateTime.Now;
        }
    }
}
