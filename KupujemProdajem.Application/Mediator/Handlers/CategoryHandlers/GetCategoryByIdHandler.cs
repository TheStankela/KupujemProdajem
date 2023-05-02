using KupujemProdajem.Application.Mediator.Queries.CategoryQueries;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Handlers.CategoryHandlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryModel>
    {
        public readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            return result;
        }
    }
}
