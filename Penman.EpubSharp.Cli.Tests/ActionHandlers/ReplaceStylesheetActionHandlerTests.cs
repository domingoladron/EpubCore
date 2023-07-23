using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class ReplaceStylesheetActionHandlerTests : ActionHandlerTestBase
    {
        public string PathToNewStylesheet = string.Empty;
        public string NameOfOldStylesheet = "0.css";

        [Fact]
        public async void TheNewStylesheetReplacesTheExistingOne()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            PathToNewStylesheet = GivenAFile("TestData/0-new.css");

            var inputStylesheetContent = await File.ReadAllTextAsync(PathToNewStylesheet);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new ReplaceStylesheetOptions
            {
                InputStylesheet = @"d:\0-new.css",
                ReplaceStylesheetName = NameOfOldStylesheet,
                InputEpub = @"d:\new.epub",
                OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\0-new.css", new MockFileData(inputStylesheetContent) },
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                var handler = new ReplaceStylesheetActionHandler(fileSystem, ConsoleWriter.Object, ResourceRetriever.Object);
                handler.HandleCliAction(options);
                

                var epubReader = EpubReader.Read(options.OutputEpub);
                var updatedStylesheet = epubReader.Resources.FindExistingStylesheet(NameOfOldStylesheet);

                updatedStylesheet.ShouldNotBeNull();
                updatedStylesheet.TextContent.ShouldBe(inputStylesheetContent);

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
            finally
            {
                File.Delete(options.OutputEpub);
            }


        }
    }
}