using MediatR;

namespace KupujemProdajem.Application.Mediator.Commands.AdCommands
{
    public class DeleteAdCommand : IRequest<bool>
    {
        public DeleteAdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
