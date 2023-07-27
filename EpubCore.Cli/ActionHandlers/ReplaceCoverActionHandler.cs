using System.IO.Abstractions;

namespace EpubCore.Cli.ActionHandlers;

public class ReplaceCoverActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    public ReplaceCoverActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter) : base(fileSystem, consoleWriter)
    {
    }

    public void HandleCliAction(object options)
    {
        if (options is not ReplaceCoverOptions replaceCoverOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(replaceCoverOptions)) return;
        if (!FileSystem.File.Exists(replaceCoverOptions.InputCoverImage))
        {
            ConsoleWriter.WriteError($"Input file not found: {replaceCoverOptions.InputCoverImage}");
            return;
        }

        var pathToCoverImage = replaceCoverOptions.InputCoverImage;

        if (TryParseImageFormat(pathToCoverImage, out var imageFormat))
        {
            if (imageFormat != null) 
                EpubWriter?.SetCover(FetchByteArrayForFile(pathToCoverImage), imageFormat.Value);

            SaveChanges(replaceCoverOptions);
        }
        else
        {
            ConsoleWriter.WriteError("Could not parse input-img to allowed types: (jpg, png, gif, svg)");
        }
    }
}