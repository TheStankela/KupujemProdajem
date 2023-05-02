using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery : IRequest<CategoryModel>
    {
        public GetCategoryByIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}