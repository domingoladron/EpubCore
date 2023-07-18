using Penman.EpubSharp.Cli.ActionHandlers;
using Penman.EpubSharp.Cli.Models;

namespace Penman.EpubSharp.Cli.Factories;

public class GetEpubDetailsFactory : IGetEpubDetailsFactory
{
    public GetEpubDetails Create(EpubBook book, ICollection<GetEpubFilterKey>? filterKeys)
    {
        return new GetEpubDetails(book, filterKeys);
    }
}