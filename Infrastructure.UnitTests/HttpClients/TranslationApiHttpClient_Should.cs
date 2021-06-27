using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Domain.Entities;
using Infrastructure.Configuration;
using Infrastructure.HttpClients;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using TestHelper;
using Xunit;

namespace Infrastructure.UnitTests.HttpClients
{
    public class TranslationApiHttpClient_Should
    {
        private const string baseUri = "https://api.funtranslations.com/translate/";
        private const string yodaEndpoint = "yoda.json/";
        private const string shakespearEndpoint = "shakespeare.json/";

        [Theory]
        [AutoMoqData]
        public async Task ReturnTranslatedText_When_GetYodaTranslationIsCalled(string text, TranslationApiResponse translatedText)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            var obj = new Root()
            {
                contents = new TestHelper.Contents()
                {
                    translated = translatedText.Contents.Translated
                }
            };

            var str = JsonConvert.SerializeObject(obj); 
            
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(str)
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri(baseUri);
            var httpClientConfiguration = new HttpClientConfiguration
            {
                YodaTranslatorEndpoint = yodaEndpoint
            };
            
            var pokerApiClient = new TranslationApiHttpClient(httpClient, httpClientConfiguration);

            var translation = await pokerApiClient.GetYodaTranslation(text);
            
            Assert.NotNull(translation);
            Assert.Equal(translation.Contents.Translated, translatedText.Contents.Translated);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnTranslatedText_When_GetShakespeareTranslationIsCalled(string text, TranslationApiResponse translatedText)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            
            var obj = new Root()
            {
                contents = new TestHelper.Contents()
                {
                    translated = translatedText.Contents.Translated
                }
            };

            var str = JsonConvert.SerializeObject(obj); 
            
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(str)
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri(baseUri);
            var httpClientConfiguration = new HttpClientConfiguration
            {
                ShakespeareTranslatorEndpoint = shakespearEndpoint
            };
            
            var pokerApiClient = new TranslationApiHttpClient(httpClient, httpClientConfiguration);

            var translation = await pokerApiClient.GetShakespeareTranslation(text);
            
            Assert.NotNull(translation);
            Assert.Equal(translation.Contents.Translated, translatedText.Contents.Translated);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}