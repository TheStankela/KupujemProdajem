using KupujemProdajem.API.Extensions;
using KupujemProdajem.Application.Mediator.Commands.AdCommands;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KupujemProdajem.Application.Mediator.Handlers.AdHandlers
{
    public class CreateAdHandler : IRequestHandler<CreateAdCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAdRepository _adRepository;

        public CreateAdHandler(IHttpContextAccessor httpContextAccessor, ICategoryRepository categoryRepository, IAdRepository adRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _categoryRepository = categoryRepository;
            _adRepository = adRepository;
        }
        public async Task<bool> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (!await _categoryRepository.CategoryExists(request.AdModel.CategoryId))
                return false;

            var category = await _categoryRepository.GetCategoryByIdAsync(request.AdModel.CategoryId);

            var adModel = new AdModel
            {
                Description = request.AdModel.Description,
                CategoryId = request.AdModel.CategoryId,
                CreatedAt = DateTime.Now,
                Price = request.AdModel.Price,
                Title = request.AdModel.Title,
                UserId = curUserId,
                Category = category
            };

            return await _adRepository.CreateAdAsync(adModel);
        }
    }
}
