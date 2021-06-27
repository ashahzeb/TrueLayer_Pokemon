using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.HttpClients;

namespace Persistence.Repositories
{
    public class TranslatorRepository : ITranslatorRepository
    {
        private readonly ITranslationApiHttpClient _translationApiHttpClient;

        public TranslatorRepository(ITranslationApiHttpClient translationApiHttpClient)
        {
            _translationApiHttpClient = translationApiHttpClient;
        }

        public async Task<string> GetShakespeareTranslation(string text)
        {
            var result = await _translationApiHttpClient.GetShakespeareTranslation(text);
            return result?.Contents.Translated ?? text;
        }
        
        public async Task<string> GetYodaTranslation(string text)
        {
            var result = await _translationApiHttpClient.GetYodaTranslation(text);
            return result?.Contents.Translated ?? text;
        }
    }
}