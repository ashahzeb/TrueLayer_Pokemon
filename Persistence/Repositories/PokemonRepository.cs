using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.HttpClients;

namespace Persistence.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonApiHttpClient _pokemonApiHttpClient;

        public PokemonRepository(IPokemonApiHttpClient pokemonApiHttpClient)
        {
            _pokemonApiHttpClient = pokemonApiHttpClient;
        }

        public async Task<Pokemon> GetPokemon(string name, string language = "en")
        {
            var pokemon = await _pokemonApiHttpClient.GetPokemonSpecies(name);
            pokemon.Name = name;
            pokemon.Description = pokemon.Flavors.FirstOrDefault(f => f.Language.Name == language)?.FlavorText;
            return pokemon;
        }
    }
}