namespace Infrastructure.Configuration
{
    public interface IHttpClientConfiguration
    {
        string PokemonApiBaseUrl { get; }
        string PokemonSpeciesEndpoint { get; }
        string TranslatorApiBaseUrl { get; }
        string ShakespeareTranslatorEndpoint { get; }
        string YodaTranslatorEndpoint { get; }
    }
}