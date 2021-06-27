using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Domain.Entities;
using Infrastructure.HttpClients;
using Moq;
using Persistence.Repositories;
using TestHelper;
using Xunit;

namespace Persistence.UnitTests.Repositories
{
    public class TranslatorRepository_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnTranslation_When_ShakespeareTranslationIsCalled_And_ApiResponseOK(
            string text,
            TranslationApiResponse response,
            [Frozen] Mock<ITranslationApiHttpClient> translationApiHttpClientMock)
        {
            translationApiHttpClientMock.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>())).ReturnsAsync(response);

            var translatorRepository = new TranslatorRepository(translationApiHttpClientMock.Object);
            
            var translation = await translatorRepository.GetShakespeareTranslation(text);
            
            Assert.Equal(response.Contents.Translated, translation);
        }

        [Theory]
        [AutoMoqData]
        public async Task ReturnSameText_When_ShakespeareTranslationIsCalled_And_ApiResponseTooManyRequests(
            string text,
            [Frozen] Mock<ITranslationApiHttpClient> translationApiHttpClientMock)
        {
            translationApiHttpClientMock.Setup(x => x.GetShakespeareTranslation(text)).ReturnsAsync((TranslationApiResponse)null);

            var translatorRepository = new TranslatorRepository(translationApiHttpClientMock.Object);
            
            var translation = await translatorRepository.GetShakespeareTranslation(text);
            
            Assert.Equal(text, translation);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task ReturnTranslation_When_YodaTranslationIsCalled_And_ApiResponseOK(
            string text,
            TranslationApiResponse response,
            [Frozen] Mock<ITranslationApiHttpClient> translationApiHttpClientMock)
        {
            translationApiHttpClientMock.Setup(x => x.GetYodaTranslation(It.IsAny<string>())).ReturnsAsync(response);

            var translatorRepository = new TranslatorRepository(translationApiHttpClientMock.Object);
            
            var translation = await translatorRepository.GetYodaTranslation(text);
            
            Assert.Equal(response.Contents.Translated, translation);
        }

        [Theory]
        [AutoMoqData]
        public async Task ReturnSameText_When_YodaTranslationIsCalled_And_ApiResponseTooManyRequests(
            string text,
            [Frozen] Mock<ITranslationApiHttpClient> translationApiHttpClientMock)
        {
            translationApiHttpClientMock.Setup(x => x.GetYodaTranslation(text)).ReturnsAsync((TranslationApiResponse)null);

            var translatorRepository = new TranslatorRepository(translationApiHttpClientMock.Object);
            
            var translation = await translatorRepository.GetYodaTranslation(text);
            
            Assert.Equal(text, translation);
        }
    }
}