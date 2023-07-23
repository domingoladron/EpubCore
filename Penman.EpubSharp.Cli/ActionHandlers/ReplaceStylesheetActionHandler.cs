using System.IO.Abstractions;
using Penman.EpubSharp.Cli.Retrievers;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class ReplaceStylesheetActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceRetriever _resourceRetriever;

        public ReplaceStylesheetActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceRetriever resourceRetriever) : base(fileSystem, consoleWriter)
        {
            _resourceRetriever = resourceRetriever;
        }

        public async void HandleCliAction(object options)
        {
            if (options is not ReplaceStylesheetOptions replaceCssOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(replaceCssOptions)) return;
            if (!FileSystem.File.Exists(replaceCssOptions.InputStylesheet))
            {
                ConsoleWriter.WriteError($"Input file not found: {replaceCssOptions.InputStylesheet}");
                return;
            }

            var existingStylesheet =
                _resourceRetriever.RetrieveCss(EpubToProcess, replaceCssOptions.ReplaceStylesheetName);
            if (existingStylesheet == null)
            {
                ConsoleWriter.WriteError($"Existing css file not found: {replaceCssOptions.ReplaceStylesheetName}");
                return;
            }

            var newCssContents = await FileSystem.File.ReadAllTextAsync(replaceCssOptions.InputStylesheet);
            existingStylesheet.TextContent = newCssContents;

            SaveChanges(replaceCssOptions);
        }
    }
}