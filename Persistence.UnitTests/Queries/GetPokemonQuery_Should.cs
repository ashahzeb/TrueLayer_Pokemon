using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.HttpClients;
using Moq;
using Persistence.Queries.GetPokemon;
using Persistence.Repositories;
using TestHelper;
using Xunit;

namespace Persistence.UnitTests.Queries
{
    public class GetPokemonQuery_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnPokemon(
            [Frozen] Mock<IPokemonRepository> pokemonRepositoryMock,
            Pokemon pokemon,
            GetPokemonQuery query,
            GetPokemonQueryHandler handler,
            CancellationToken cancellationToken)
        {
            pokemon.Name = query.Name;
            pokemon.Description = pokemon.Flavors.FirstOrDefault(fte => fte.Language.Name == "en")?.FlavorText;
            pokemonRepositoryMock.Setup(x => x.GetPokemon(query.Name, "en")).ReturnsAsync(pokemon);

            var result = await handler.Handle(query, cancellationToken);
            
            Assert.Equal(result.Name, pokemon.Name);
            Assert.Equal(result.IsLegendary, pokemon.IsLegendary);
            Assert.Equal(result.Description, pokemon.Description);
        }
    }
}