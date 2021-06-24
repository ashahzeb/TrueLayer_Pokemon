using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Infrastructure.Configuration;
using Infrastructure.HttpClients;
using Moq;
using Moq.Protected;
using Xunit;

namespace Infrastructure.UnitTests.HttpClients
{
    public class TranslationApiHttpClient_Should
    {
        private const string baseUri = "https://api.funtranslations.com/translate/";
        private const string yodaEndpoint = "yoda.json/";
        private const string shakespearEndpoint = "shakespeare.json/";

        [Theory]
        [AutoData]
        public async Task ReturnTranslatedText_When_GetYodaTranslationIsCalled(string text, string translatedText)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(translatedText)
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
            
            Assert.Equal(translation, translatedText);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnTranslatedText_When_GetShakespeareTranslationIsCalled(string text, string translatedText)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(translatedText)
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
            
            Assert.Equal(translation, translatedText);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnSameText_When_GetYodaTranslationIsCalled_And_ResponseIsTooManyRequest(string text)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.TooManyRequests
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
            
            Assert.Equal(translation, text);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnSameText_When_GetShakespeareTranslationIsCalled_And_ResponseIsTooManyRequest(string text)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.TooManyRequests
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
            
            Assert.Equal(translation, text);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }

    }
}