namespace EpubCore.Cli.Models;

public class HelpDocEntry
{
    public string VerbName { get; set; } = string.Empty;
    public string HelpText { get; set; } = string.Empty;

    public List<HelpParamEntry> Parameters { get; set; } = new List<HelpParamEntry>();
}