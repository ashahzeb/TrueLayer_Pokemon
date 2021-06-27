using MediatR;

namespace Persistence.Queries.GetTranslation
{
    public record GetShakespeareTranslationQuery(string Text) : IRequest<string> 
    {
    }
}