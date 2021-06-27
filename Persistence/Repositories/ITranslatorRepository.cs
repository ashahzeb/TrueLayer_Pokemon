using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Repositories
{
    public interface ITranslatorRepository
    {
        Task<string> GetShakespeareTranslation(string text);
        Task<string> GetYodaTranslation(string text);
    }
}