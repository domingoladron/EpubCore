using System.IO.Abstractions.TestingHelpers;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
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
                InputCoverImage = @"d:\cover.jpg",
                InputEpub = @"d:\new.epub",
                OutputEpub = @"d:\new-1.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\cover.jpg", new MockFileData(newCoverContents) },
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                
                var handler = new ReplaceCoverActionHandler(fileSystem);
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