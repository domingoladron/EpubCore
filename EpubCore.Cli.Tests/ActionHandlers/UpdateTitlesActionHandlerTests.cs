using System.IO.Abstractions.TestingHelpers;
using EpubCore.Cli.ActionHandlers;
using Shouldly;

namespace EpubCore.Cli.Tests.ActionHandlers
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
                InputEpub = @$"{ExecutingPath}/new.epub",
                OutputEpub = @$"{ExecutingPath}/new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @$"{ExecutingPath}/new.epub", new MockFileData(epubContent) }
                });
                var handler = new UpdateTitlesActionHandler(fileSystem, ConsoleWriter.Object);
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