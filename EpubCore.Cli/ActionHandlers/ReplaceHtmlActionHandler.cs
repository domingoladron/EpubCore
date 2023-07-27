using System.IO.Abstractions;
using EpubCore.Cli.Managers;

namespace EpubCore.Cli.ActionHandlers
{
    public class ReplaceHtmlActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceManager _resourceManager;

        public ReplaceHtmlActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceManager resourceManager) : base(fileSystem, consoleWriter)
        {
            _resourceManager = resourceManager;
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
                _resourceManager.RetrieveHtml(EpubToProcess!, replaceHtmlOptions.ReplaceHtmlName);
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