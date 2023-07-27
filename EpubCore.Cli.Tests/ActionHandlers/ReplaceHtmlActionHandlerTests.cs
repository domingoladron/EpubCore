using System.IO.Abstractions.TestingHelpers;
using EpubCore.Cli.ActionHandlers;
using Shouldly;

namespace EpubCore.Cli.Tests.ActionHandlers
{
    public class ReplaceHtmlActionHandlerTests : ActionHandlerTestBase
    {
        public string PathToNewHtml = string.Empty;
        public string NameOfOldHtml = "2000430189831848965_2500-h-0.htm.xhtml";

        [Fact]
        public async void TheNewHtmlReplacesTheExistingOne()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            PathToNewHtml = GivenAFile("TestData/0-new.html");

            var inputHtmlContent = await File.ReadAllTextAsync(PathToNewHtml);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new ReplaceHtmlOptions()
            {
                InputHtml=@$"{ExecutingPath}/0-new.html",
                ReplaceHtmlName= NameOfOldHtml,
                InputEpub =@$"{ExecutingPath}/new.epub",
                OutputEpub = @$"{ExecutingPath}/new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    {@$"{ExecutingPath}/0-new.html", new MockFileData(inputHtmlContent) },
                    {@$"{ExecutingPath}/new.epub", new MockFileData(epubContent) }
                });
                var handler = new ReplaceHtmlActionHandler(fileSystem, ConsoleWriter.Object, ResourceRetriever.Object);
                handler.HandleCliAction(options);
                

                var epubReader = EpubReader.Read(options.OutputEpub);
                var updatedHtml = epubReader.Resources.FindExistingHtml(NameOfOldHtml);

                updatedHtml.ShouldNotBeNull();
                updatedHtml.TextContent.ShouldBe(inputHtmlContent);

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