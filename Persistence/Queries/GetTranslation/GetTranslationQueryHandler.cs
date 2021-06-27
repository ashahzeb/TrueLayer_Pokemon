using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.Repositories;

namespace Persistence.Queries.GetTranslation
{
    public class GetTranslationQueryHandler : IRequestHandler<GetShakespeareTranslationQuery, string>,
        IRequestHandler<GetYodaTranslationQuery, string>
    {
        private readonly ITranslatorRepository _translatorRepository;

        public GetTranslationQueryHandler(ITranslatorRepository translatorRepository)
        {
            _translatorRepository = translatorRepository;
        }

        public async Task<string> Handle(GetShakespeareTranslationQuery request, CancellationToken cancellationToken)
        {
            return await _translatorRepository.GetShakespeareTranslation(request.Text);
        }

        public async Task<string> Handle(GetYodaTranslationQuery request, CancellationToken cancellationToken)
        {
            return await _translatorRepository.GetYodaTranslation(request.Text);
        }
    }
}