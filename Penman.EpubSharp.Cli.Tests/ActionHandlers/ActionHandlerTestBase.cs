using System.Reflection;
using AutoFixture;
using Moq;
using Penman.EpubSharp.Cli.Retrievers;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers;

public class ActionHandlerTestBase
{
    protected const string TestEPub = "TestData/test1.epub";
    protected const string TestEPubResult = "test1-result.epub";
    protected Fixture Fixture = new Fixture();
    protected Mock<IConsoleWriter> ConsoleWriter;
    protected Mock<EpubResourceRetriever> ResourceRetriever;
    protected string PathToTestEpub { get; set; }

    protected ActionHandlerTestBase()
    {
        ConsoleWriter = new Mock<IConsoleWriter>();
        ResourceRetriever = new Mock<EpubResourceRetriever>();
    }

    public string GivenAnEpub(string epubName)
    {
        return GivenAFile(epubName);
    }

    public string GivenAFile(string fileName)
    {
        return Path.Join(AppDomain.CurrentDomain.BaseDirectory, fileName);
    }

    public string GivenATempFile(string fileName)
    {
        return Path.Join(Path.GetTempPath(), fileName);
    }
}