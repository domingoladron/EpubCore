using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using EpubCore.Cli.ActionHandlers;
using EpubCore.Cli.Factories;
using EpubCore.Cli.Models;
using Moq;

namespace EpubCore.Cli.Tests.ActionHandlers
{
    public class GetEpubDetailsActionHandlerTests : ActionHandlerTestBase
    {
        public string PathToNewCover = string.Empty;

        [Fact]
        public void TheDetailsAreRetrievedInJson()
        {
            TheDetailsAreRetrievedInExpectedFormat(OutputFormat.Json);
        }

        [Fact]
        public void TheDetailsAreRetrievedInYaml()
        {
            TheDetailsAreRetrievedInExpectedFormat(OutputFormat.Yaml);
        }

        private async void TheDetailsAreRetrievedInExpectedFormat(OutputFormat outputFormat)
        {
            PathToTestEpub = GivenAFile(TestEPub);
            var epubContent = await File.ReadAllBytesAsync(PathToTestEpub);
            var messageSerialiser = new Mock<IMessageSerialiser>();
            var factory = new Mock<IGetEpubDetailsFactory>();
            var epubDetails = Fixture.Create<GetEpubDetails>();

            factory.Setup(g => g.Create(It.IsAny<EpubBook>(), new List<GetEpubFilterKey>()))
                .Returns(epubDetails);

            var options = new GetEpubDetailsOptions()
            {
                InputEpub = @"d:\new.epub",
                Filter = Fixture.Create<List<GetEpubFilterKey>>(),
                OutputFormat = outputFormat
            };

            try
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"d:\new.epub", new MockFileData(epubContent) }
                });
                
                var handler = new GetEpubDetailsActionHandler(fileSystem, ConsoleWriter.Object, messageSerialiser.Object, factory.Object);
                handler.HandleCliAction(options);


                messageSerialiser.Verify(g => g.Serialise(It.IsAny<GetEpubDetails>(), outputFormat), Times.Once);

                factory.Verify(g => g.Create(It.IsAny<EpubBook>(), It.IsAny<ICollection<GetEpubFilterKey>>()), Times.Once);

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}