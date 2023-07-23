using System.IO.Abstractions;
using JasperFx.Core;
using Penman.EpubSharp.Cli.Managers;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetHtmlActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IEpubResourceManager _epubResourceManager;
    public GetHtmlActionHandler(
        IFileSystem fileSystem, 
        IConsoleWriter consoleWriter,
        IEpubResourceManager epubResourceManager) : base(fileSystem, consoleWriter)
    {
        _epubResourceManager = epubResourceManager;
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetHtmlOptions getHtmlOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getHtmlOptions)) return;

        var htmlFile = _epubResourceManager.RetrieveHtml(EpubToProcess, getHtmlOptions.HtmlFileName);

        if (htmlFile == null)
        {
            ConsoleWriter.WriteError($"Could not find an html file in this epub with the name '{getHtmlOptions.HtmlFileName}'");
            return;
        }

        ConsoleWriter.WriteSuccess(htmlFile.TextContent);
    }
}