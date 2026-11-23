namespace lab4.Commands.Base;

public interface ICommand
{
    string GetInfo();
    void Action();
}