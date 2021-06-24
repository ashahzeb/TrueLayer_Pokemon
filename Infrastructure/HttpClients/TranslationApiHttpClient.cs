using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Infrastructure.Configuration;

namespace Infrastructure.HttpClients
{
    public class TranslationApiHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientConfiguration _httpClientConfiguration;
        
        public TranslationApiHttpClient(HttpClient httpClient, IHttpClientConfiguration httpClientConfiguration)
        {
            _httpClient = httpClient;
            _httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<string> GetShakespeareTranslation(string text)
        {
            return await Translate(_httpClientConfiguration.ShakespeareTranslatorEndpoint, text);
        }
        
        public async Task<string> GetYodaTranslation(string text)
        {
            return await Translate(_httpClientConfiguration.YodaTranslatorEndpoint, text);
        }

        private async Task<string> Translate(string endpoint, string text)
        {
            HttpResponseMessage response = null;
            try
            {
                var payload = new {text};
                response =
                    await _httpClient.PostAsJsonAsync($"{endpoint}{text.Trim()}", payload);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                if (response?.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return text;
                }

                throw;
            }
        }
    }
}