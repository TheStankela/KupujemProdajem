using MediatR;

namespace KupujemProdajem.Application.Mediator.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
