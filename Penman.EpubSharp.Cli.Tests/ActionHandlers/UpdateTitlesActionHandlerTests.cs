using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class UpdateTitlesActionHandlerTests : ActionHandlerTestBase
    {
        [Fact]
        public async void TheNewTitlesReplaceTheExistingOnes()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            var newTitles = new List<string>() { "Title", "SubTitle" };
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new UpdateTitlesOptions()
            {
                Titles = newTitles,
                InputEpub = @"d:\new.epub",
                OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                var handler = new UpdateTitlesActionHandler(fileSystem);
                handler.HandleCliAction(options);
                
                var epubReader = EpubReader.Read(options.OutputEpub);
                var titles = epubReader.Titles;

                titles.ShouldNotBeEmpty();
                titles.Count.ShouldBe(2);
                titles.ShouldBe(newTitles);
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