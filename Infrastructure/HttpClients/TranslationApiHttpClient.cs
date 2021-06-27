using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Configuration;

namespace Infrastructure.HttpClients
{
    public class TranslationApiHttpClient : ITranslationApiHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientConfiguration _httpClientConfiguration;
        
        public TranslationApiHttpClient(HttpClient httpClient, IHttpClientConfiguration httpClientConfiguration)
        {
            _httpClient = httpClient;
            _httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<TranslationApiResponse> GetShakespeareTranslation(string text)
        {
            return await Translate(_httpClientConfiguration.ShakespeareTranslatorEndpoint, text);
        }
        
        public async Task<TranslationApiResponse> GetYodaTranslation(string text)
        {
            return await Translate(_httpClientConfiguration.YodaTranslatorEndpoint, text);
        }

        private async Task<TranslationApiResponse> Translate(string endpoint, string text)
        {
            var payload = new {text};
            var response = await _httpClient.PostAsJsonAsync($"{endpoint}", payload);

            using var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TranslationApiResponse>(contentStream,
                new JsonSerializerOptions());
        }
    }
}