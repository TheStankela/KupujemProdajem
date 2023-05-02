using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Queries.AdQueries
{
    public class GetAllAdsQuery : IRequest<IEnumerable<AdModel>>
    {
    }
}
