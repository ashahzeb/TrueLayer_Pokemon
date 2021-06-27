using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Infrastructure.HttpClients;
using Moq;
using Moq.Protected;
using TestHelper;
using Xunit;

namespace Infrastructure.UnitTests.HttpClients
{
    public class PokemonApiHttpClient_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnStatusCodeOk_When_GetPokemonSpeciesIsCalled_And_PokemonExists(IPokemonApiHttpClient pokerApiClient)
        {
            var pokemonSpecies = await pokerApiClient.GetPokemonSpecies("pikachu");
            
            Assert.NotNull(pokemonSpecies);
        }
        
        [Theory]
        [AutoData]
        public async Task ReturnStatusCodeNotFound_When_GetPokemonSpeciesIsCalled_And_PokemonDoesNotExist(string name)
        {
            var (pokerApiClient, handlerMock) = TestDataBuilder.CreatePokemonApiHttpClient(HttpStatusCode.NotFound);

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