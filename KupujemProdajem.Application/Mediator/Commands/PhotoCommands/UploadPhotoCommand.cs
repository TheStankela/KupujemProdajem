using MediatR;
using Microsoft.AspNetCore.Http;

namespace KupujemProdajem.Application.Mediator.Commands.PhotoCommands
{
    public class UploadPhotoCommand : IRequest<bool>
    {
        public UploadPhotoCommand(List<IFormFile> pictureFiles, int advertisementId)
        {
            PictureFiles = pictureFiles;
            AdvertisementId = advertisementId;
        }

        public List<IFormFile> PictureFiles { get; set; }
        public int AdvertisementId { get; set; }
    }
}
