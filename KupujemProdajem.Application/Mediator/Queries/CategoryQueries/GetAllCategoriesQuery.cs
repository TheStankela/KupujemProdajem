
using KupujemProdajem.Domain.Models;
using MediatR;

namespace KupujemProdajem.Application.Mediator.Queries.CategoryQueries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryModel>>
    {
    }
}
