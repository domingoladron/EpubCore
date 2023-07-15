using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class ReplaceCoverActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    public ReplaceCoverActionHandler(IFileSystem fileSystem) : base(fileSystem)
    {
    }

    public void HandleCliAction(object options)
    {
        if (options is not ReplaceCoverOptions replaceCoverOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(replaceCoverOptions)) return;
        if (!FileSystem.File.Exists(replaceCoverOptions.InputCoverImage))
        {
            Console.WriteLine($"Input file not found: {replaceCoverOptions.InputCoverImage}");
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
            Console.WriteLine("Could not parse input-img to allowed types: (jpg, png, gif, svg)");
            return;
        }
    }
}