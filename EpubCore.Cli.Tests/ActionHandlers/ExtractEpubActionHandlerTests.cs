using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using EpubCore.Cli.ActionHandlers;
using Moq;

namespace EpubCore.Cli.Tests.ActionHandlers
{
    public class ExtractEpubActionHandlerTests : ActionHandlerTestBase
    {
        private readonly Mock<IFileExtractor> _fileExtractor = new Mock<IFileExtractor>();

        [Fact]
        public async void TheNewCoverReplacesTheExistingOne()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);
            
            var extractPath = @$"{ExecutingPath}/new-{Guid.NewGuid()}";
            var options = new ExtractEpubOptions()
            {
                InputEpub = @$"{ExecutingPath}/new.epub",
                DestinationDirectory = extractPath
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { extractPath, new MockDirectoryData() },
                    { @$"{ExecutingPath}/new.epub", new MockFileData(epubContent) }
                });

                var handler = new ExtractEPubActionHandler(fileSystem, ConsoleWriter.Object, _fileExtractor.Object);
                handler.HandleCliAction(options);

                _fileExtractor.Verify(g => g.ExtractToDirectory(options.InputEpub, options.DestinationDirectory), Times.Once);

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}