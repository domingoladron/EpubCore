using System.IO.Abstractions;
using System.IO.Compression;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class ExtractEPubActionHandler : EpubActionHandlerBase, ICliActionHandler
{

    public ExtractEPubActionHandler(IFileSystem fileSystem) : base(fileSystem)
    {
    }

    public void HandleCliAction(object options)
    {
        if (options is not ExtractEpubOptions extractEpubOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(extractEpubOptions)) return;

        using var archive = ZipFile.OpenRead(extractEpubOptions.InputEpub);
        if (!FileSystem.Directory.Exists(extractEpubOptions.DestinationDirectory))
            FileSystem.Directory.CreateDirectory(extractEpubOptions.DestinationDirectory);
        
        archive.ExtractToDirectory(extractEpubOptions.DestinationDirectory);

        Console.WriteLine($"Extract to {extractEpubOptions.DestinationDirectory} complete");
    }
}