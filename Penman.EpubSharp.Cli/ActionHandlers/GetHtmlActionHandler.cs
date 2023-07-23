using System.IO.Abstractions;
using JasperFx.Core;
using Penman.EpubSharp.Cli.Retrievers;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetHtmlActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IEpubResourceRetriever _epubResourceRetriever;
    public GetHtmlActionHandler(
        IFileSystem fileSystem, 
        IConsoleWriter consoleWriter,
        IEpubResourceRetriever epubResourceRetriever) : base(fileSystem, consoleWriter)
    {
        _epubResourceRetriever = epubResourceRetriever;
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetHtmlOptions getHtmlOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getHtmlOptions)) return;

        var htmlFile = _epubResourceRetriever.RetrieveHtml(EpubToProcess, getHtmlOptions.HtmlFileName);

        if (htmlFile == null)
        {
            ConsoleWriter.WriteError($"Could not find an html file in this epub with the name '{getHtmlOptions.HtmlFileName}'");
            return;
        }

        ConsoleWriter.WriteSuccess(htmlFile.TextContent);
    }
}