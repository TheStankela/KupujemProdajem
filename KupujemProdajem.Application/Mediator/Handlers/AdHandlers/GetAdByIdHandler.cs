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
    public class GetAdByIdHandler : IRequestHandler<GetAdByIdQuery, AdModel?>
    {
        public GetAdByIdHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public readonly IAdRepository _adRepository;

        public async Task<AdModel?> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _adRepository.GetAdByIdAsync(request.Id);
            return result;
        }
    }
}
