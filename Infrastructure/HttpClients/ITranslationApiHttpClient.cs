using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.HttpClients
{
    public interface ITranslationApiHttpClient
    {
        Task<TranslationApiResponse> GetYodaTranslation(string text);

        Task<TranslationApiResponse> GetShakespeareTranslation(string text);
    }
}