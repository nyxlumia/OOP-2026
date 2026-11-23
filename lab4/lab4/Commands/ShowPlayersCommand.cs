using lab4.Commands.Base;
using lab4.Service.Base;
using Lab4.Service.Views;

namespace lab4.Commands
{

    public class ShowPlayersCommand : ICommand
    {
        private IAccountService _accountService;

        public ShowPlayersCommand (IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string GetInfo()
        {
            return "View all players";
        }

        public void Action()
        {
            var players = _accountService.GetAll();
            foreach (var player in players)
            {
                var view = new PlayerView
                {
                    Id = player.Id,
                    UserName = player.UserName,
                    AccountType = player.GetType().Name,
                    Rating = player.CurrentRating
                };
                Console.WriteLine(view);
            }
        }
    }
}