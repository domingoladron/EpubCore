using System.IO.Abstractions;
using Penman.EpubSharp.Cli.Retrievers;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class ReplaceHtmlActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceRetriever _resourceRetriever;

        public ReplaceHtmlActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceRetriever resourceRetriever) : base(fileSystem, consoleWriter)
        {
            _resourceRetriever = resourceRetriever;
        }

        public async void HandleCliAction(object options)
        {
            if (options is not ReplaceHtmlOptions replaceHtmlOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(replaceHtmlOptions)) return;
            if (!FileSystem.File.Exists(replaceHtmlOptions.InputHtml))
            {
                ConsoleWriter.WriteError($"Input file not found: {replaceHtmlOptions.InputHtml}");
                return;
            }

            var existingHtml =
                _resourceRetriever.RetrieveHtml(EpubToProcess, replaceHtmlOptions.ReplaceHtmlName);
            if (existingHtml == null)
            {
                ConsoleWriter.WriteError($"Existing html file not found: {replaceHtmlOptions.ReplaceHtmlName}");
                return;
            }

            var newHtmlContents = await FileSystem.File.ReadAllTextAsync(replaceHtmlOptions.InputHtml);
            existingHtml.TextContent = newHtmlContents;

            SaveChanges(replaceHtmlOptions);
        }
    }
}