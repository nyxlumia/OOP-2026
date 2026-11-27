namespace lab5.DTO
{
    public class SteamAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public List<int> WishlistGameIds { get; private set; } = new List<int>();

        private decimal _accountBalance;
        public decimal Balance
        {
            get { return _accountBalance; }
            protected set { _accountBalance = value < 0 ? 0 : value; }
        }

        public SteamAccount(string userName, string password, decimal startBalance = 0)
        {
            UserName = userName;
            Password = password;
            Balance = startBalance;
        }

        public virtual void TopUpBalance(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public virtual void TopLoseBalance(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (Balance < amount) throw new InvalidOperationException("You do not have enough money.");
            Balance -= amount;
        }

        internal void SetBalance(decimal balance)
        {
            Balance = balance;
        }
    }
}