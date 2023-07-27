using System.IO.Abstractions;

namespace EpubCore.Cli.ActionHandlers;

public class ExtractEPubActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IFileExtractor _fileExtractor;

    public ExtractEPubActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IFileExtractor fileExtractor) : base(fileSystem, consoleWriter)
    {
        _fileExtractor = fileExtractor;
    }

    public void HandleCliAction(object options)
    {
        if (options is not ExtractEpubOptions extractEpubOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(extractEpubOptions)) return;

        if (_fileExtractor.ExtractToDirectory(extractEpubOptions.InputEpub, extractEpubOptions.DestinationDirectory))
        {
            ConsoleWriter.WriteSuccess($"Extract to {extractEpubOptions.DestinationDirectory} complete");
        }
        else
        {
            ConsoleWriter.WriteError($"Failed to extract to {extractEpubOptions.DestinationDirectory}");
        }
    }
}