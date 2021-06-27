using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.HttpClients
{
    public interface IPokemonApiHttpClient
    {
        Task<Pokemon> GetPokemonSpecies(string name);
    }
}