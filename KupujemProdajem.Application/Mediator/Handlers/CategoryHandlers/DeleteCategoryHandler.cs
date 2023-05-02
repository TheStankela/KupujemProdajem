using KupujemProdajem.Application.Mediator.Commands.CategoryCommands;
using KupujemProdajem.Domain.Repositories;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await _categoryRepository.CategoryExists(request.Id))
                return false;

            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            return await _categoryRepository.DeleteCategory(categoryToDelete);
        }
    }
}
