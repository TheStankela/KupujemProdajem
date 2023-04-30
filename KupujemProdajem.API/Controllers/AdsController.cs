using IdentityServer4.AccessTokenValidation;
using KupujemProdajem.API.Extensions;
using KupujemProdajem.API.Models;
using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdById(int id)
        {
            var ads = await _adRepository.GetAdByIdAsync(id);
            return Ok(ads);
        }
        [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetAdsByUserId(string userId)
        {
            var ads = await _adRepository.GetAdsByUserId(userId);
            return Ok(ads);
        }
        [HttpGet("Categories/{categoryId}")]
        public async Task<IActionResult> GetAdsByCategory(int categoryId)
        {
            var ads = await _adRepository.GetAdsByCategory(categoryId);
            return Ok(ads);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAdd([FromBody] CreateAdModel adDto)
        {
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
        [Authorize]
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
