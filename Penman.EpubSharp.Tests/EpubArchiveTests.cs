using Xunit;

namespace Penman.EpubSharp.Tests
{
    public class EpubArchiveTests
    {
        [Fact]
        public void FindEntryTest()
        {

            var pathToEPubFile = Cwd.Combine("Samples/Bogtyven.epub");
            var archive = new EpubArchive(pathToEPubFile);
            Assert.NotNull(archive.FindEntry("META-INF/container.xml"));
            Assert.Null(archive.FindEntry("UNEXISTING_ENTRY"));

        }
    }
}
