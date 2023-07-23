using System.IO.Abstractions.TestingHelpers;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class RemoveHtmlActionHandlerTests : ActionHandlerTestBase
    {
        public string NameOfOldHtml = "2000430189831848965_2500-h-0.htm.xhtml";

        [Fact]
        public async void TheHtmlIsRemovedFromTheEPub()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);
            var options = new RemoveHtmlOptions()
            {
                RemoveItemName= NameOfOldHtml,
                InputEpub = @"d:\new.epub",
                OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                var handler = new RemoveHtmlActionHandler(fileSystem, ConsoleWriter.Object, ResourceRetriever.Object);
                handler.HandleCliAction(options);
                
                var epubReader = EpubReader.Read(options.OutputEpub);
                var updatedHtml = epubReader.Resources.FindExistingHtml(NameOfOldHtml);
            }
            catch (EpubParseException ex)
            {
                ex.ShouldNotBeNull();
                ex.Message.ShouldContain($"{NameOfOldHtml}");
            }
            finally
            {
                File.Delete(options.OutputEpub);
            }
        }
    }
}