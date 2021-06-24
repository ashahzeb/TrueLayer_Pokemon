using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.HttpClients
{
    public interface IPokemonApiHttpClient
    {
        Task<HttpResponseMessage> GetPokemonSpecies(string name);
    }
}