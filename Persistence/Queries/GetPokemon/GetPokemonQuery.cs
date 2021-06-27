using Domain.Abstraction;
using MediatR;

namespace Persistence.Queries.GetPokemon
{
    public record GetPokemonQuery (string Name) : IRequest<IPokemon>
    {
    }
}