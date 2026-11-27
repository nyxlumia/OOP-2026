namespace lab5.DTO
{
    public abstract class StoreGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal BasePrice { get; }
        private int _keysCount;
        public int KeysCount
        {
            get => _keysCount;
            set
            {
                if (value < 0) throw new ArgumentException("Keys count cannot be negative.");
                _keysCount = value;
            }
        }
        public abstract decimal GetPrice();

        public StoreGame(string title, decimal price, int keysCount)
        {
            if (price < 0)
            {
                throw new ArgumentException("The game price cannot be negative.");
            }
            Title = title;
            BasePrice = price;
            KeysCount = keysCount;
        }
    }

    
    public class DiscountGame : StoreGame
    {
        public DiscountGame(string title, decimal price, int keysCount) : base(title, price, keysCount) { }
        private const decimal DiscountMultiplier = 0.5m; // 50% discount
        public override decimal GetPrice()
        {
            return BasePrice * DiscountMultiplier; 
        }
    }
    public class StandardGame : StoreGame
    {
        public StandardGame(string title, decimal price, int keysCount) : base(title, price, keysCount) { }
        public override decimal GetPrice()
        {
            return BasePrice;
        }
    }

    public class DeluxeGame : StoreGame
    {
        public DeluxeGame(string title, decimal price, int keysCount) : base(title, price, keysCount) { }
        private const decimal DeluxeSurcharge = 500m; // +500 for deluxe edition
        public override decimal GetPrice()
        {
            return BasePrice + DeluxeSurcharge;
        }
    }
}