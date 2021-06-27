using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Repositories
{
    public interface IPokemonRepository
    {
        Task<Pokemon> GetPokemon(string name, string language = "en");
    }
}