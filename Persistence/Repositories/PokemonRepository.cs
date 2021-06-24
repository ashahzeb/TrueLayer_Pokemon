using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Abstraction;
using Domain.Entity;
using Infrastructure.HttpClients;

namespace Persistence.Repositories
{
    public class PokemonRepository
    {
        private readonly IPokemonApiHttpClient _pokemonApiHttpClient;

        public PokemonRepository(IPokemonApiHttpClient pokemonApiHttpClient)
        {
            _pokemonApiHttpClient = pokemonApiHttpClient;
        }

        public async Task<IPokemon> GetPokemon(string name, string language = "en")
        {
            var httpResponseMessage = await _pokemonApiHttpClient.GetPokemonSpecies(name);

            IPokemon pokemon;
            
            try
            {
                using (var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(contentStream, new JsonSerializerOptions());
                }
                
                pokemon.Name = name;
                pokemon.Description = pokemon.Flavors.FirstOrDefault(fte => fte.Language.Name == language)?.FlavorText;
                return pokemon;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}