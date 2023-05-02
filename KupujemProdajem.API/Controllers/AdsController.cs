using KupujemProdajem.API.Models;
using MediatR;
using KupujemProdajem.Application.Mediator.Queries.AdQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KupujemProdajem.Application.Mediator.Commands;
using KupujemProdajem.Application.Mediator.Commands.AdCommands;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAds()
        {
            var query = new GetAllAdsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdById(int id)
        {
            var query = new GetAdByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetAdsByUserId(string userId)
        {
            var query = new GetAdsByUserIdQuerry(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("Categories/{categoryId}")]
        public async Task<IActionResult> GetAdsByCategory(int categoryId)
        {
            var query = new GetAdsByCategoryQuery(categoryId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAd([FromBody] CreateAdModel adDto)
        {
            var command = new CreateAdCommand(adDto);
            var result = await _mediator.Send(command);
            return result == true ? Ok("Successfully created!") : BadRequest("Something went wrong.");
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAd([FromQuery] int advertisementId)
        {
            var command = new DeleteAdCommand(advertisementId);
            var result = await _mediator.Send(command);
            return result == true ? Ok("Successfully deleted.") : BadRequest();
        }
    }
}
