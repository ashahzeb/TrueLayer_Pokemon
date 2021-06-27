using MediatR;

namespace Persistence.Queries.GetTranslation
{
    public record GetYodaTranslationQuery(string Text) : IRequest<string> 
    {
    }
}