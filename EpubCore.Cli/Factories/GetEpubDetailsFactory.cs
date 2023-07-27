using EpubCore.Cli.ActionHandlers;
using EpubCore.Cli.Models;

namespace EpubCore.Cli.Factories;

public class GetEpubDetailsFactory : IGetEpubDetailsFactory
{
    public GetEpubDetails Create(EpubBook book, ICollection<GetEpubFilterKey>? filterKeys)
    {
        return new GetEpubDetails(book, filterKeys);
    }
}