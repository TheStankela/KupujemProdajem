using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Queries.PhotoQueries
{
    public class GetAdPhotosQuery : IRequest<List<PhotoModel>>
    {
        public GetAdPhotosQuery(int adId)
        {
            AdId = adId;
        }
        public int AdId { get; set; }
    }
}
