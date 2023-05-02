using KupujemProdajem.Application.Mediator.Commands.CategoryCommands;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Handlers.CategoryHandlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        public readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryModel = new CategoryModel { Name = request.CategoryModel.CategoryName };
            return await _categoryRepository.CreateCategory(categoryModel);
        }
    }
}
