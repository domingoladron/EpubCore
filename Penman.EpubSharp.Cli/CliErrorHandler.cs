using CommandLine;

namespace Penman.EpubSharp.Cli;

public class CliErrorHandler : ICliErrorHandler
{
    public void HandleError(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine(error.Tag);
        }
    }
}