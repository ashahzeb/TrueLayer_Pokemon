using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RestAPI.IntegrationTests.Controllers
{
    public class PokemonController_Should
    {
        private class Pokemon
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public string Habitat { get; set; }
            public bool IsLegendary { get; set; }
        }
        
        private class ErrorResponseBody
        {
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public List<string> Errors { get; set; }
            public object Data { get; set; }
        }

        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        private HttpClient GetClient()
        {
            if (_factory == null)
                _factory = new WebApplicationFactory<Startup>();

            if (_client == null)
                _client = _factory.CreateClient();
            
            return _client;
        }

        [Theory]
        [InlineData("pikachu", false)]
        public async Task ReturnPokemon_When_GetPokemonIsCalled_And_PokemonExists(string name, bool isLegendary)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"/pokemon/{name}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string content = response.Content.ReadAsStringAsync().Result;
            Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });

            Assert.Equal(name, pokemon.Name, true);
            Assert.NotNull(pokemon.Habitat);
            Assert.NotNull(pokemon.Description);
            Assert.Equal(pokemon.IsLegendary, isLegendary);
        }

        [Theory]
        [AutoData]
        public async Task ReturnNotFound_When_GetPokemonIsCalled_And_PokemonDoesNotExist(string name)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"/pokemon/{name}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            ErrorResponseBody content = JsonSerializer.Deserialize<ErrorResponseBody>(response.Content.ReadAsStringAsync().Result);
            Assert.False(content.Succeeded);
            Assert.NotNull(content.Message);
        }

        [Theory]
        [InlineData("pikachu", false)]
        public async Task ReturnPokemonWithTranslatedDescription_When_GetTranslatedPokemonIsCalled_And_PokemonExists(string name, bool isLegendary)
        {
            HttpResponseMessage response = GetClient().GetAsync($"/pokemon/translated/{name}").Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string content = response.Content.ReadAsStringAsync().Result;
            Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });

            Assert.Equal(name, pokemon.Name, true);
            Assert.NotNull(pokemon.Habitat);
            Assert.NotNull(pokemon.Description);
            Assert.Equal(pokemon.IsLegendary, isLegendary);
        }

        [Theory]
        [AutoData]
        public async Task ReturnNotFound_When_GetTranslatedPokemonIsCalled_And_PokemonDoesNotExist(string name)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"/pokemon/translated/{name}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            ErrorResponseBody content = JsonSerializer.Deserialize<ErrorResponseBody>(response.Content.ReadAsStringAsync().Result);
            Assert.False(content.Succeeded);
            Assert.NotNull(content.Message);
        }
    }
}