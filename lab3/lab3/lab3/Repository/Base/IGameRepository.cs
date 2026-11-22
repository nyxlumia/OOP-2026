using lab3.DTO;

namespace lab3.Repository
{

    public interface IGameRepository
    {
        public List<GameResult> GetAll();
        public GameResult? GetById(int id);
        public void Add(GameResult game);
        public void Delete(int id);
        public void Update(GameResult game);
    }
}