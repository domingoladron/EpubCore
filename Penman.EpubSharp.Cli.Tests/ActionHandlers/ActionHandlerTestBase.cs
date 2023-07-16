using System.Reflection;

namespace Penman.EpubSharp.Cli.Tests.ActionHandlers;

public class ActionHandlerTestBase
{
    protected const string TestEPub = "TestData/test1.epub";
    protected const string TestEPubResult = "test1-result.epub";
    protected string PathToTestEpub { get; set; }

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