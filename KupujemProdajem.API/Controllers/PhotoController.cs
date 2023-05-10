using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Application.Mediator.Commands.PhotoCommands;
using KupujemProdajem.Application.Mediator.Queries.PhotoQueries;
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
        public async Task<IActionResult> UploadPhotos(List<IFormFile> pictures, int advertisementId)
        {
            var command = new UploadPhotoCommand(pictures, advertisementId);
            var result = await _mediator.Send(command);

            return result == true ? Ok("Upload succeeded.") : BadRequest("Upload unsuccessfull.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAdPhotos(int adId)
        {
            var query = new GetAdPhotosQuery(adId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
