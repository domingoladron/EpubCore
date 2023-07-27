using EpubCore.Cli.ActionHandlers;
using EpubCore.Cli.Models;

namespace EpubCore.Cli.Factories;

/// <summary>
/// A factory for constructing our GetEpubDetails data
/// </summary>
public interface IGetEpubDetailsFactory
{
    GetEpubDetails Create(EpubBook book, ICollection<GetEpubFilterKey>? filterKeys);
}