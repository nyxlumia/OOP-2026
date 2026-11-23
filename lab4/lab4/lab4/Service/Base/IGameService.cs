using lab4.DTO;

namespace lab4.Service.Base
{
    public interface IGameService
    {
        public void Create(int player1Id, int player2Id, BaseGame game, bool isPlayer1Winner);
        public GameResult? GetById(int id);
        public List<GameResult> GetAll();
        List<GameResult> ReadPlayerGamesByPlayerId(int playerId);

    }
}