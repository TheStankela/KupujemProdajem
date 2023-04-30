using CloudinaryDotNet.Actions;
using KupujemProdajem.API.Extensions;
using KupujemProdajem.API.Models;
using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using KupujemProdajem.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAdRepository _adRepository;
        private readonly IPhotoService _photoService;

        public AdsController(ICategoryRepository categoryRepository, IHttpContextAccessor contextAccessor, IAdRepository adRepository, IPhotoService photoService)
        {
            _categoryRepository = categoryRepository;
            _contextAccessor = contextAccessor;
            _adRepository = adRepository;
            _photoService = photoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAds()
        {
            var result = await _adRepository.GetAllAdsAsync();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAdd([FromBody] CreateAdModel adDto)
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return BadRequest("You must be logged in.");

            var curUserId = _contextAccessor.HttpContext.User.GetUserId();

            var category = await _categoryRepository.GetCategoryByIdAsync(adDto.CategoryId);
            if (category == null)
                return NotFound("Category not found.");


            var adModel = new AdModel
            {
                Description = adDto.Description,
                CategoryId = adDto.CategoryId,
                CreatedAt = DateTime.Now,
                Price = adDto.Price,
                Title = adDto.Title,
                UserId = curUserId,
                Category = category
            };

            if (!await _adRepository.CreateAdAsync(adModel))
                return BadRequest("Something went wrong while saving.");

            return Ok("Successfully added.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAd([FromQuery] int advertisementId)
        {
            var adToDelete = await _adRepository.GetAdByIdAsync(advertisementId);
            if (adToDelete == null) return NotFound();

            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            if (adToDelete.UserId != curUserId)
                return Forbid();

            if (!await _adRepository.DeleteAd(adToDelete))
                return BadRequest("Something went wrong");

            return Ok("Successfully deleted");
        }
    }
}
