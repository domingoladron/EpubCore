using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetEpubDetailsActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    public GetEpubDetailsActionHandler(IFileSystem fileSystem) : base(fileSystem)
    {
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetEpubDetailsOptions getEpubDetailsOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getEpubDetailsOptions)) return;

        if (getEpubDetailsOptions.Filter.Any())
        {
            Console.WriteLine("You have entered the following filter criteria:");
            foreach (var curFilter in getEpubDetailsOptions.Filter)
            {
                Console.WriteLine($"     * {curFilter}");
            }
        }
        else
        {
            Console.WriteLine("Retrieving all EPub Details");
        }
    }
}