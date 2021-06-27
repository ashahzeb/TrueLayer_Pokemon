using System.Threading;
using System.Threading.Tasks;
using Domain.Abstraction;
using MediatR;
using Persistence.Repositories;

namespace Persistence.Queries.GetPokemon
{
    public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, IPokemon>
    {
        private readonly IPokemonRepository _pokemonRepository;
        
        public GetPokemonQueryHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<IPokemon> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {
            return await _pokemonRepository.GetPokemon(request.Name);
        }
    }
}