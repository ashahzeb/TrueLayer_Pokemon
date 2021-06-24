using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.HttpClients;
using Moq;
using Moq.Protected;

namespace TestHelper
{
    public static class TestDataBuilder
    {
        private const string baseUri = "https://pokeapi.co/api/v2/";
        private const string pokemonSpeciesEndpoint = "pokemon-species/";
        
        public static (PokemonApiHttpClient, Mock<HttpMessageHandler>) CreatePokemonApiHttpClient(
            HttpStatusCode statusCode, string responseContent = "")
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage(statusCode);

            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                response.Content = new StringContent(responseContent);
            }

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

            return (new PokemonApiHttpClient(httpClient, httpClientConfiguration), handlerMock);
        }
    }
}