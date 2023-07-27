using System.IO.Abstractions.TestingHelpers;
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

            var extractPath = @$"d:\new-{Guid.NewGuid()}";
            var options = new ExtractEpubOptions()
            {
                InputEpub = @"d:\new.epub",
                DestinationDirectory = extractPath
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { extractPath, new MockDirectoryData() },
                    { @"d:\new.epub", new MockFileData(epubContent) }
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