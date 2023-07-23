using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using Moq;
using Penman.EpubSharp.Cli.ActionHandlers;
using Penman.EpubSharp.Cli.Managers;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class GetHtmlActionHandlerTests : ActionHandlerTestBase
    {
        private readonly Mock<IEpubResourceManager> _epubResourceRetriever = new();
        private EpubTextFile? _htmlFile = null;

        [Fact]
        public void TheHtmlIsRetrieved()
        {
            TheHtmlContentsAreSought(true);
        }

        [Fact]
        public void TheHtmlIsNotFound()
        {
            TheHtmlContentsAreSought(false);
        }

        private async void TheHtmlContentsAreSought(bool htmlFound)
        {
            PathToTestEpub = GivenAFile(TestEPub);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);
            var htmlFileName = Fixture.Create<string>();
            _htmlFile = htmlFound ? Fixture.Create<EpubTextFile>() : null;

            _epubResourceRetriever.Setup(g => g.RetrieveHtml(It.IsAny<EpubBook>(), htmlFileName))
                .Returns(_htmlFile);

            var options = new GetHtmlOptions()
            {
                InputEpub = @"d:\new.epub",
                HtmlFileName = htmlFileName
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                
                var handler = new GetHtmlActionHandler(fileSystem, ConsoleWriter.Object, _epubResourceRetriever.Object);
                handler.HandleCliAction(options);

                _epubResourceRetriever.Verify(g => g.RetrieveHtml(It.IsAny<EpubBook>(), htmlFileName), Times.Once);

                if (htmlFound)
                {
                    ConsoleWriter.Verify(g => g.WriteSuccess(_htmlFile.TextContent), Times.Once);
                }
                else
                {
                    ConsoleWriter.Verify(g => g.WriteError(It.IsAny<string>()), Times.Once);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}