using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("get-html", HelpText = "Get contents of html file")]
public class GetHtmlOptions : EpubInputOptionsBase
{
    [Option('h', "html-file", Required = true, HelpText = "The nme of the html file for which to fetch the contents")]
    public string HtmlFileName { get; set; }

}