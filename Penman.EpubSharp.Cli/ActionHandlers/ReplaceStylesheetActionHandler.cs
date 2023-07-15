using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class ReplaceStylesheetActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public ReplaceStylesheetActionHandler(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public async void HandleCliAction(object options)
        {
            if (options is not ReplaceStylesheetOptions replaceCssOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(replaceCssOptions)) return;
            if (!FileSystem.File.Exists(replaceCssOptions.InputStylesheet))
            {
                Console.WriteLine($"Input file not found: {replaceCssOptions.InputStylesheet}");
                return;
            }

            var existingStylesheet =
                EpubToProcess.Resources.FindExistingStylesheet(replaceCssOptions.ReplaceStylesheetName);
            if (existingStylesheet == null)
            {
                Console.WriteLine($"Existing css file not found: {replaceCssOptions.ReplaceStylesheetName}");
                return;
            }

            var newCssContents = await FileSystem.File.ReadAllTextAsync(replaceCssOptions.InputStylesheet);
            existingStylesheet.TextContent = newCssContents;

            SaveChanges(replaceCssOptions);
        }
    }
}