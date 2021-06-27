using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
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

        public async Task<Pokemon> GetPokemonSpecies(string name)
        {
            HttpResponseMessage response = null;
            try
            {
                response =
                    await _httpClient.GetAsync(
                        $"{_httpClientConfiguration.PokemonSpeciesEndpoint}{name.ToLower().Trim()}");

                response.EnsureSuccessStatusCode();

                await using var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Pokemon>(contentStream, new JsonSerializerOptions());
            }
            catch (JsonException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                if (response?.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
                }

                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}