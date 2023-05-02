using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Application.Mediator.Commands.PhotoCommands;
using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PhotoController(IMediator mediator)
        {
           _mediator = mediator;
        }
        [Authorize]
        [HttpPost("{advertisementId}")]
        public async Task<IActionResult> UploadPhotos(List<IFormFile> pictureFiles, int advertisementId)
        {
            var command = new UploadPhotoCommand(pictureFiles, advertisementId);
            var result = await _mediator.Send(command);

            return result == true ? Ok("Upload succeeded.") : BadRequest("Upload unsuccessfull.");
        }
    }
}
