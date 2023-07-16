using System.IO.Abstractions;
using Penman.EpubSharp.Cli.ActionHandlers;
using Shouldly;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers
{
    public class ReplaceStylesheetActionHandlerTests : ActionHandlerTestBase
    {
        public string PathToNewStylesheet { get; set; }
        public string NameOfOldStylesheet = "0.css";
        [Fact]
        public async void TheNewStylesheetReplacesTheExistingOne()
        {
            PathToTestEpub = GivenAFile(TestEPub);
            PathToNewStylesheet = GivenAFile("TestData/0-new.css");

            var inputStylesheetContent = await File.ReadAllTextAsync(PathToNewStylesheet);

            var options = new ReplaceStylesheetOptions
            {
                InputStylesheet = PathToNewStylesheet,
                ReplaceStylesheetName = NameOfOldStylesheet,
                InputEpub = PathToTestEpub,
                OutputEpub = GivenATempFile(TestEPubResult)
            };

            try
            {
                var handler = new ReplaceStylesheetActionHandler(new FileSystem());
                handler.HandleCliAction(options);
                

                var epubReader = EpubReader.Read(options.OutputEpub);
                var updatedStylesheet = epubReader.Resources.FindExistingStylesheet(NameOfOldStylesheet);

                updatedStylesheet.ShouldNotBeNull();
                updatedStylesheet.TextContent.ShouldBe(inputStylesheetContent);

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
            finally
            {
                File.Delete(TestEPubResult);
            }


        }
    }
}