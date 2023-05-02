using KupujemProdajem.Application.Mediator.Queries;
using KupujemProdajem.Application.Mediator.Queries.AdQueries;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.AdHandlers
{
    internal class GetAllAdsHandler : IRequestHandler<GetAllAdsQuery, IEnumerable<AdModel>>
    {
        public readonly IAdRepository _adRepository;
        public GetAllAdsHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }
        public async Task<IEnumerable<AdModel>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
        {
            var result = await _adRepository.GetAllAdsAsync();
            return result;
        }
    }
}
