using System.IO.Abstractions;
using System.IO.Compression;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class ExtractEPubActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IFileExtractor _fileExtractor;

    public ExtractEPubActionHandler(IFileSystem fileSystem, IFileExtractor fileExtractor) : base(fileSystem)
    {
        _fileExtractor = fileExtractor;
    }

    public void HandleCliAction(object options)
    {
        if (options is not ExtractEpubOptions extractEpubOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(extractEpubOptions)) return;

        if (_fileExtractor.ExtractToDirectory(extractEpubOptions.InputEpub, extractEpubOptions.DestinationDirectory))
        {
            Console.WriteLine($"Extract to {extractEpubOptions.DestinationDirectory} complete");
        }
        else
        {
            Console.WriteLine($"Failed to extract to {extractEpubOptions.DestinationDirectory}");
        }

       
    }
}