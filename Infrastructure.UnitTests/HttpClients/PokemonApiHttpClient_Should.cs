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
    public class PokemonApiHttpClient_Should
    {
        private const string baseUri = "https://pokeapi.co/api/v2/";
        private const string pokemonSpeciesEndpoint = "pokemon-species/";
        
        [Fact]
        public async Task ReturnStatusCodeOk_When_GetPokemonSpeciesIsCalled_And_PokemonExists()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
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
                PokemonSpeciesEndpoint = pokemonSpeciesEndpoint
            };
            
            var pokerApiClient = new PokemonApiHttpClient(httpClient, httpClientConfiguration);

            var pokemonSpecies = await pokerApiClient.GetPokemonSpecies("pikachu");
            
            Assert.NotNull(pokemonSpecies);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnStatusCodeNotFound_When_GetPokemonSpeciesIsCalled_And_PokemonDoesNotExist(string name)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            var httpClientConfiguration = new HttpClientConfiguration
            {
                PokemonSpeciesEndpoint = "pokemon-species/"
            };
            
            var pokerApiClient = new PokemonApiHttpClient(httpClient, httpClientConfiguration);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => pokerApiClient.GetPokemonSpecies(name));
            
            Assert.True(exception.StatusCode == HttpStatusCode.NotFound);
            
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}