using System.Threading.Tasks;

namespace Infrastructure.HttpClients
{
    public interface ITranslationApiHttpClient
    {
        Task<string> GetYodaTranslation(string text);

        Task<string> GetShakespeareTranslation(string text);
    }
}