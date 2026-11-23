using lab4.Commands.Base;
using System;

namespace lab4.Commands;

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