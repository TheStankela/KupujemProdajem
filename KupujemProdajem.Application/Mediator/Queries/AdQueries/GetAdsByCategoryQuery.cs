using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Queries.AdQueries
{
    public class GetAdsByCategoryQuery : IRequest<List<AdModel>>
    {
        public GetAdsByCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}
