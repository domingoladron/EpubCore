using System.IO.Abstractions.TestingHelpers;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class UpdateAuthorsActionHandlerTests : ActionHandlerTestBase
    {
        readonly List<string> _newAuthors = new() { "Author1", "Author2" };
        [Fact]
        public async void TheNewAuthorsReplaceTheExistingOnes()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var options = new UpdateAuthorsOptions()
            {
                Authors = _newAuthors,
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
                var handler = new UpdateAuthorsActionHandler(fileSystem, ConsoleWriter.Object);
                handler.HandleCliAction(options);
                
                var epubReader = EpubReader.Read(options.OutputEpub);
                var authors = epubReader.Authors.ToList();

                authors.ShouldNotBeEmpty();
                authors.Count.ShouldBe(2);
                authors.ShouldBe(_newAuthors);
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
        public async void TheNewAuthorsAreAddedToTheExistingOnes()
        {
            PathToTestEpub = GivenAFile(TestEPub);

            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);

            var epubBook = EpubReader.Read(PathToTestEpub);
            var currentAuthors = epubBook.Authors;

            var options = new UpdateAuthorsOptions()
            {
                Authors = _newAuthors,
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
                
                var handler = new UpdateAuthorsActionHandler(fileSystem, ConsoleWriter.Object);
                handler.HandleCliAction(options);

                epubBook = EpubReader.Read(options.OutputEpub);
                var authors = epubBook.Authors.ToList();

                var allAuthors = currentAuthors.ToList();
                allAuthors.AddRange(_newAuthors.ToList());


                authors.ShouldNotBeEmpty();
                authors.Count.ShouldBe(allAuthors.Count);
                authors.ShouldBe(allAuthors);
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