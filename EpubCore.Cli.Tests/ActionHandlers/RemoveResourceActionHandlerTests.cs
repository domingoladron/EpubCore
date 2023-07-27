using System.IO.Abstractions.TestingHelpers;
using EpubCore.Cli.ActionHandlers;
using Shouldly;

namespace EpubCore.Cli.Tests.ActionHandlers;

public class RemoveResourceActionHandlerTests : ActionHandlerTestBase
{
    public const string NameOfHtml = "2000430189831848965_2500-h-0.htm.xhtml";
    public const string NameOfCss = "0.css";
    private readonly MockFileSystem _fileSystem;

    public RemoveResourceActionHandlerTests()
    {
        PathToTestEpub = GivenAFile(TestEPub);
        var epubContent = File.ReadAllBytesAsync(PathToTestEpub).Result;
        _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { @"d:\new.epub", new MockFileData(epubContent) }
        });
    }

    [Fact]
    public void TheHtmlIsRemovedFromTheEPub()
    {
        var options = new RemoveResourceOptions()
        {
            RemoveItemName= NameOfHtml,
            EpubResourceType = EpubResourceType.Html,
            InputEpub = @"d:\new.epub",
            OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
        };

        try
        {
           
            var handler = new RemoveResourceActionHandler(_fileSystem, ConsoleWriter.Object, ResourceRetriever.Object);
            handler.HandleCliAction(options);
                
            var epubReader = EpubReader.Read(options.OutputEpub);
                
            epubReader.Resources.FindExistingHtml(NameOfHtml);
        }
        catch (EpubParseException ex)
        {
            ex.ShouldNotBeNull();
            ex.Message.ShouldContain($"{NameOfHtml}");
        }
        finally
        {
            File.Delete(options.OutputEpub);
        }
    }

    [Fact]
    public void TheCssIsRemovedFromTheEPub()
    {
        var options = new RemoveResourceOptions()
        {
            RemoveItemName = NameOfCss,
            EpubResourceType = EpubResourceType.Css,
            InputEpub = @"d:\new.epub",
            OutputEpub = @$"d:\new-{Guid.NewGuid()}.epub"
        };

        try
        {
          
            var handler = new RemoveResourceActionHandler(_fileSystem, ConsoleWriter.Object, ResourceRetriever.Object);
            handler.HandleCliAction(options);

            var epubReader = EpubReader.Read(options.OutputEpub);

            epubReader.Resources.FindExistingStylesheet(NameOfHtml);
        }
        catch (EpubParseException ex)
        {
            ex.ShouldNotBeNull();
            ex.Message.ShouldContain($"{NameOfCss}");
        }
        finally
        {
            File.Delete(options.OutputEpub);
        }
    }
}