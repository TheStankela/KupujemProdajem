using KupujemProdajem.Application.Mediator.Queries;
using KupujemProdajem.Application.Mediator.Queries.AdQueries;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.AdHandlers
{
    public class GetAdsByCategoryHandler : IRequestHandler<GetAdsByCategoryQuery, List<AdModel>>
    {
        private readonly IAdRepository _adRepository;

        public GetAdsByCategoryHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }
        public async Task<List<AdModel>> Handle(GetAdsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _adRepository.GetAdsByCategory(request.CategoryId);
            return result;
        }
    }
}
