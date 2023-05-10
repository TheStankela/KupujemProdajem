using KupujemProdajem.Application.Mediator.Queries.PhotoQueries;
using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.PhotoHandlers
{
    internal class GetAdPhotosHandler : IRequestHandler<GetAdPhotosQuery, List<PhotoModel>>
    {
        private readonly IPhotoRepository _photoRepository;

        public GetAdPhotosHandler(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }
        public async Task<List<PhotoModel>> Handle(GetAdPhotosQuery request, CancellationToken cancellationToken)
        {
            return await _photoRepository.GetAdPhotos(request.AdId);
        }
    }
}
