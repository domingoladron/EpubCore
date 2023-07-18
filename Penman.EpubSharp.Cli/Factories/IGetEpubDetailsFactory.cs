using Penman.EpubSharp.Cli.ActionHandlers;
using Penman.EpubSharp.Cli.Models;

namespace Penman.EpubSharp.Cli.Factories;

/// <summary>
/// A factory for constructing our GetEpubDetails data
/// </summary>
public interface IGetEpubDetailsFactory
{
    GetEpubDetails Create(EpubBook book, ICollection<GetEpubFilterKey>? filterKeys);
}