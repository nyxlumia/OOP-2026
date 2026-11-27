using lab5.Commands.Base;

namespace lab5.Commands
{
    public class ExitCommand : ICommand
    {
        public ExitCommand()
        {
        }
        public string GetInfo()
        {
            return "Exit Program";
        }

        public void Action()
        {
            Environment.Exit(0);
        }
    }
}