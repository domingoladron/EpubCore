namespace Penman.EpubSharp.Cli;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteSuccess(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteError(string message)
    {
        Console.WriteLine(message);
    }
}