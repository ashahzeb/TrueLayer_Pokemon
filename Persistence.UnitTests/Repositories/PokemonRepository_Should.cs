using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Domain.Abstraction;
using Persistence.Repositories;
using TestHelper;
using Xunit;

namespace Persistence.UnitTests.Repositories
{
    public class PokemonRepository_Should
    {
        private static string GetPokemonSampleResponse()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetName().Name + ".Resources.PokemonApiResponse.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [Theory]
        [InlineData("Pikachu")]
        public async Task ReturnPokemon_When_ApiResponseIsCorrect(string name)
        {
            var (client, mockHandler) = TestDataBuilder.CreatePokemonApiHttpClient(HttpStatusCode.OK, GetPokemonSampleResponse());

            var repository = new PokemonRepository(client);
            IPokemon pokemon = await repository.GetPokemon(name);

            Assert.NotNull(pokemon);
            Assert.Equal(pokemon.Name, name);
        }

        [Theory]
        [AutoData]
        public async Task ThrowException_When_ApiResponseHasADifferentStructure(string name, string response)
        {
            var (client, mockHandler) = TestDataBuilder.CreatePokemonApiHttpClient(HttpStatusCode.OK, response);

            var repository = new PokemonRepository(client);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => repository.GetPokemon(name));
            
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }
    }
}