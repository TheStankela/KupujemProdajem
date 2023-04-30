using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public readonly IAdRepository _adRepository;
        private readonly IPhotoService _photoService;
        private readonly IPhotoRepository _photoRepository;

        public PhotoController(IAdRepository adRepository, IPhotoService photoService, IPhotoRepository photoRepository)
        {
            _adRepository = adRepository;
            _photoService = photoService;
            _photoRepository = photoRepository;
        }
        [Authorize]
        [HttpPost("{advertisementId}")]
        public async Task<IActionResult> UploadPhotos(List<IFormFile> pictureFiles, int advertisementId)
        {
            var ad = await _adRepository.GetAdByIdAsync(advertisementId);

            if (pictureFiles.Count > 0)
            {
                foreach (var item in pictureFiles)
                {
                    var itemUrl = await _photoService.AddPhotoAsync(item);
                    var photoToAdd = new PhotoModel
                    {
                        Ad = ad,
                        Url = itemUrl.Url.ToString()
                    };
                    await _photoRepository.AddPhoto(photoToAdd);
                }
                return Ok("File upload completed.");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
