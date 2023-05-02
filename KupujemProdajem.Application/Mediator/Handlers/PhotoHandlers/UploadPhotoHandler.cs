using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Application.Mediator.Commands.PhotoCommands;
using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.PhotoHandlers
{
    public class UploadPhotoHandler : IRequestHandler<UploadPhotoCommand, bool>
    {
        public readonly IAdRepository _adRepository;
        private readonly IPhotoService _photoService;
        private readonly IPhotoRepository _photoRepository;
        public UploadPhotoHandler(IAdRepository adRepository, IPhotoService photoService, IPhotoRepository photoRepository)
        {
            _adRepository = adRepository;
            _photoService = photoService;
            _photoRepository = photoRepository;
        }
        public async Task<bool> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var ad = await _adRepository.GetAdByIdAsync(request.AdvertisementId);
            if (ad == null)
                return false;

            if (request.PictureFiles.Count > 0)
            {
                foreach (var item in request.PictureFiles)
                {
                    var uploadResult = await _photoService.AddPhotoAsync(item);
                    var photoToAdd = new PhotoModel
                    {
                        Ad = ad,
                        Url = uploadResult.Url.ToString()
                    };
                    await _photoRepository.AddPhoto(photoToAdd);
                }
                return true;
            }
            return false;
            
        }
    }
}
