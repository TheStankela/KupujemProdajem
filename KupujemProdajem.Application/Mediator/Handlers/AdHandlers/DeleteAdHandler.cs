using KupujemProdajem.API.Extensions;
using KupujemProdajem.Application.Mediator.Commands.AdCommands;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Handlers.AdHandlers
{
    public class DeleteAdHandler : IRequestHandler<DeleteAdCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdRepository _adRepository;
        public DeleteAdHandler(IAdRepository adRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adRepository = adRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            
            var adToDelete = await _adRepository.GetAdByIdAsync(request.Id);
            if (adToDelete == null) return false;

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (adToDelete.UserId != curUserId)
                return false;

            return await _adRepository.DeleteAd(adToDelete);
        }
    }
}
