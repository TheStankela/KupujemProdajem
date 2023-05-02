using KupujemProdajem.API.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Commands.AdCommands
{
    public class CreateAdCommand : IRequest<bool>
    {
        public CreateAdCommand(CreateAdModel adModel)
        {
            AdModel = adModel;
        }

        public CreateAdModel AdModel { get; set; }
    }
}
