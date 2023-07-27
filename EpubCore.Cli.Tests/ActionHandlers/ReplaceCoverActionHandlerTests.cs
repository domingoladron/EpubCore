using System.IO.Abstractions.TestingHelpers;
using EpubCore.Cli.ActionHandlers;
using Shouldly;

namespace EpubCore.Cli.Tests.ActionHandlers
{
    public class ReplaceCoverActionHandlerTests : ActionHandlerTestBase
    {
        public string PathToNewCover = string.Empty;

        [Fact]
        public async void TheNewCoverReplacesTheExistingOne()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            PathToNewCover = GivenAFile("TestData/cover.jpg");

            var newCoverContents = await File.ReadAllBytesAsync(PathToNewCover);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new ReplaceCoverOptions()
            {
                InputCoverImage = @$"{ExecutingPath}/cover.jpg",
                InputEpub = @$"{ExecutingPath}/new.epub",
                OutputEpub = @$"{ExecutingPath}/new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @$"{ExecutingPath}/cover.jpg", new MockFileData(newCoverContents) },
                    { @$"{ExecutingPath}/new.epub", new MockFileData(epubContent) }
                });
                
                var handler = new ReplaceCoverActionHandler(fileSystem, ConsoleWriter.Object);
                handler.HandleCliAction(options);
                

                var epubReader = EpubReader.Read(options.OutputEpub);
                var updatedCoverImageContents = epubReader.CoverImage;

                updatedCoverImageContents.ShouldNotBeNull();
                updatedCoverImageContents.ShouldBe(newCoverContents);

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