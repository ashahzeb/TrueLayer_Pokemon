using System.Threading;
using System.Threading.Tasks;
using Moq;
using Persistence.Queries.GetTranslation;
using Persistence.Repositories;
using TestHelper;
using Xunit;

namespace Persistence.UnitTests.Queries
{
    public class GetTranslationQueryHandler_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnTranslation_When_GetShakespeareTranslationQueryIsCalled(
            string translatedText,
            GetShakespeareTranslationQuery query,
            CancellationToken cancellationToken,
            Mock<ITranslatorRepository> translatorRepositoryMock)
        {
            translatorRepositoryMock.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>()))
                .ReturnsAsync(translatedText);

            var handler = new GetTranslationQueryHandler(translatorRepositoryMock.Object);

            var result = await handler.Handle(query, cancellationToken);
            
            Assert.Equal(translatedText, result);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task ReturnTranslation_When_GetYodaTranslationQueryIsCalled(
            string translatedText,
            GetYodaTranslationQuery query,
            CancellationToken cancellationToken,
            Mock<ITranslatorRepository> translatorRepositoryMock)
        {
            translatorRepositoryMock.Setup(x => x.GetYodaTranslation(It.IsAny<string>()))
                .ReturnsAsync(translatedText);

            var handler = new GetTranslationQueryHandler(translatorRepositoryMock.Object);

            var result = await handler.Handle(query, cancellationToken);
            
            Assert.Equal(translatedText, result);
        }
    }
}