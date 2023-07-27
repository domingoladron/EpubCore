using System.IO.Abstractions;
using System.IO.Compression;

namespace EpubCore.Cli;

public interface IFileExtractor
{
    bool ExtractToDirectory(string pathToExtractableItem, string destinationDirectory);
}

public class FileExtractor : IFileExtractor
{
    private readonly IFileSystem _fileSystem;
    public FileExtractor(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
    public bool ExtractToDirectory(string pathToExtractableItem, string destinationDirectory)
    {
        try
        {
            using var archive = ZipFile.OpenRead(pathToExtractableItem);
            if (!_fileSystem.Directory.Exists(destinationDirectory))
                _fileSystem.Directory.CreateDirectory(destinationDirectory);

            archive.ExtractToDirectory(destinationDirectory);
            return true;
        }
        catch
        {
            return false;
        }
    }
}