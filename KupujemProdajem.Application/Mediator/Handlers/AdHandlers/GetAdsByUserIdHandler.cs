using KupujemProdajem.Application.Mediator.Queries;
using KupujemProdajem.Application.Mediator.Queries.AdQueries;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Handlers.AdHandlers
{
    public class GetAdsByUserIdHandler : IRequestHandler<GetAdsByUserIdQuerry, List<AdModel>>
    {
        private readonly IAdRepository _adRepository;

        public GetAdsByUserIdHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }
        public async Task<List<AdModel>> Handle(GetAdsByUserIdQuerry request, CancellationToken cancellationToken)
        {
            var result = await _adRepository.GetAdsByUserId(request.UserId);
            return result;
        }
    }
}
