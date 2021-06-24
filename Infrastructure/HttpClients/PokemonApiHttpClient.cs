using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Configuration;

namespace Infrastructure.HttpClients
{
    public class PokemonApiHttpClient : IPokemonApiHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientConfiguration _httpClientConfiguration;
        
        public PokemonApiHttpClient(HttpClient httpClient, IHttpClientConfiguration httpClientConfiguration)
        {
            _httpClient = httpClient;
            _httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<HttpResponseMessage> GetPokemonSpecies(string name)
        {
            var response =
                    await _httpClient.GetAsync(
                        $"{_httpClientConfiguration.PokemonSpeciesEndpoint}{name.ToLower().Trim()}");

            return response.EnsureSuccessStatusCode();
        }
    }
}