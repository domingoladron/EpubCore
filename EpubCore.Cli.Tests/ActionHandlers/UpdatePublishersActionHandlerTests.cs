using System.IO.Abstractions.TestingHelpers;
using EpubCore.Cli.ActionHandlers;
using Shouldly;

namespace EpubCore.Cli.Tests.ActionHandlers
{
    public class UpdatePublishersActionHandlerTests : ActionHandlerTestBase
    {
        readonly List<string> _newPublishers = new() { "Publisher1", "Publisher2" };
        [Fact]
        public async void TheNewPublishersReplaceTheExistingOnes()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new UpdatePublishersOptions()
            {
                Publishers = _newPublishers,
                ClearPrevious = true,
                InputEpub = @"d:\new.epub",
                OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                var handler = new UpdatePublishersActionHandler(fileSystem, ConsoleWriter.Object);
                handler.HandleCliAction(options);
                
                var epubReader = EpubReader.Read(options.OutputEpub);
                var publishers = epubReader.Publishers.ToList();

                publishers.ShouldNotBeEmpty();
                publishers.Count.ShouldBe(_newPublishers.Count);
                publishers.ShouldBe(_newPublishers);
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

        [Fact]
        public async void TheNewPublishersAreAddedToTheExistingOnes()
        {
            PathToTestEpub = GivenAFile(TestEPub);

            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var epubBook = EpubReader.Read(PathToTestEpub);
            var currentPublishers = epubBook.Publishers;

            var options = new UpdatePublishersOptions()
            {
                Publishers = _newPublishers,
                ClearPrevious = false,
                InputEpub = @"d:\new.epub",
                OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                
                var handler = new UpdatePublishersActionHandler(fileSystem, ConsoleWriter.Object);
                handler.HandleCliAction(options);

                epubBook = EpubReader.Read(options.OutputEpub);
                var publishers = epubBook.Publishers.ToList();

                var allPublishers = currentPublishers.ToList();
                allPublishers.AddRange(_newPublishers.ToList());


                publishers.ShouldNotBeEmpty();
                publishers.Count.ShouldBe(allPublishers.Count);
                publishers.ShouldBe(allPublishers);
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