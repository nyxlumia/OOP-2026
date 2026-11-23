namespace lab4.DTO
{
    public abstract class BaseGame
    {
        public int BaseRating { get; }
        public abstract int GetRating();
        public BaseGame(int rating)
        {
            if (rating < 1)
            {
                throw new ArgumentException("The game rating must be greater than 1.");
            }
            BaseRating = rating;
        }
    }

    public class TrainingGame : BaseGame
    {
        public TrainingGame(int rating) : base(rating) { }
        public override int GetRating()
        {
            return 0;
        }
    }
    public class StandardGame : BaseGame
    {
        public StandardGame(int rating) : base(rating) { }
        public override int GetRating()
        {
            return BaseRating * 20;
        }
    }

    public class RankedGame : BaseGame
    {
        public RankedGame(int rating) : base(rating) { }
        public override int GetRating()
        {
            return BaseRating * 50;
        }
    }

    public class GameFactory
    {
        public BaseGame CreateGame(string type, int rating)
        {
            switch (type)
            {
                case "standard": return new StandardGame(rating);
                case "training": return new TrainingGame(rating);
                case "ranked": return new RankedGame(rating);
                default: throw new ArgumentException($"Unknown game type: {type}");
            }
        }
    }
}
