using CommandLine;

namespace Penman.EpubSharp.Cli;

/// <summary>
/// An error handler for cli errors
/// </summary>
public interface ICliErrorHandler
{
    void HandleError(IEnumerable<Error> errors);
}