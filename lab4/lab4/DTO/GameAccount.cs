namespace lab4.DTO
{
    public class GameAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        private int _currentRating;
        public int CurrentRating
        {
            get { return _currentRating; }
            protected set { _currentRating = value < 1 ? 1 : value; }
        }

        public GameAccount(string userName, int startRating = 100)
        {
            UserName = userName;
            CurrentRating = startRating;
        }

        public virtual void WinGame(string opponent, BaseGame game)
        {
            CurrentRating += game.GetRating();
        }

        public virtual void LoseGame(string opponent, BaseGame game)
        {
            CurrentRating -= game.GetRating();
        }

        public void SetRating(int rating)
        {
            CurrentRating = rating;
        }
    }

    public class BeginnerGameAccount : GameAccount
    {
        public BeginnerGameAccount(string userName, int startRating = 100) : base(userName, startRating) { }
        public override void WinGame(string opponent, BaseGame game)
        {
            if (CurrentRating < 1000)
            {
                CurrentRating += game.GetRating() * 2;
            }
            else
            {
                base.WinGame(opponent, game);
            }
        }
        public override void LoseGame(string opponent, BaseGame game)
        {
            if (CurrentRating < 1000)
            {
                CurrentRating -= game.GetRating() / 2;
            }
            else
            {
                base.LoseGame(opponent, game);
            }
        }
    }

    public class AverageGameAccount : GameAccount
    {
        public AverageGameAccount(string userName, int startRating = 1000) : base(userName, startRating) { }
        public override void WinGame(string opponent, BaseGame game)
        {
            if (CurrentRating >= 1000 && CurrentRating <= 1000000)
            {
                CurrentRating += game.GetRating() * 10;
            }
            else
            {
                base.WinGame(opponent, game);
            }
        }
        public override void LoseGame(string opponent, BaseGame game)
        {
            if (CurrentRating >= 1000 && CurrentRating <= 1000000)
            {
                CurrentRating -= game.GetRating() / 10;
            }
            else
            {
                base.LoseGame(opponent, game);
            }
        }
    }

    public class ProGameAccount : GameAccount
    {
        public ProGameAccount(string userName, int startRating = 1000000) : base(userName, startRating) { }
        public override void WinGame(string opponent, BaseGame game)
        {
            if (CurrentRating >= 1000000)
            {
                CurrentRating += game.GetRating() * 30;
            }
            else
            {
                base.WinGame(opponent, game);
            }
        }
        public override void LoseGame(string opponent, BaseGame game)
        {
            if (CurrentRating >= 1000000)
            {
                CurrentRating -= game.GetRating() / 30;
            }
            else
            {
                base.LoseGame(opponent, game);
            }
        }
    }
}