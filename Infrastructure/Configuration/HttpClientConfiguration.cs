namespace Infrastructure.Configuration
{
    public class HttpClientConfiguration : IHttpClientConfiguration
    {
        public string PokemonApiBaseUrl { get; set; }
        public string PokemonSpeciesEndpoint { get; set; }
        public string TranslatorApiBaseUrl { get; set; }
        public string ShakespeareTranslatorEndpoint { get; set; }
        public string YodaTranslatorEndpoint { get; set; }
    }
}