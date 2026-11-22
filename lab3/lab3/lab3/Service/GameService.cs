using lab3.DTO;
using lab3.Repository;
using lab3.Service.Base;

namespace lab3.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IAccountRepository _accountRepository;

        public GameService(IGameRepository gameRepository, IAccountRepository accountRepository)
        {
            _gameRepository = gameRepository;
            _accountRepository = accountRepository;
        }

        public void Create(int player1Id, int player2Id, BaseGame game, bool isPlayer1Winner)
        {
            var p1 = _accountRepository.GetById(player1Id);
            var p2 = _accountRepository.GetById(player2Id);

            if (p1 != null && p2 != null)
            {
                if (isPlayer1Winner)
                {
                    p1.WinGame(p2.UserName, game);
                    p2.LoseGame(p1.UserName, game);
                }
                else
                {
                    p1.LoseGame(p2.UserName, game);
                    p2.WinGame(p1.UserName, game);
                }

                _accountRepository.Update(p1);
                _accountRepository.Update(p2);

                int gameId = _gameRepository.GetAll().Count > 0 ? _gameRepository.GetAll().Max(x => x.GameID) + 1 : 1;

                _gameRepository.Add(new GameResult(p2.UserName, game.GetRating(), p1.Id, isPlayer1Winner, game.GetType().Name) { GameID = gameId });
                _gameRepository.Add(new GameResult(p1.UserName, game.GetRating(), p2.Id, !isPlayer1Winner, game.GetType().Name) { GameID = gameId });
            }
        }

        public GameResult? GetById(int id)
        {
            return _gameRepository.GetById(id);
        }

        public List<GameResult> GetAll()
        {
            return _gameRepository.GetAll();
        }

        public List<GameResult> ReadPlayerGamesByPlayerId(int playerId)
        {
            return _gameRepository.GetAll().Where(x => x.PlayerId == playerId).ToList();
        }
    }
}