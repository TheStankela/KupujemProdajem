using KupujemProdajem.API.Models;
using KupujemProdajem.Application.Mediator.Commands.CategoryCommands;
using KupujemProdajem.Application.Mediator.Queries.CategoryQueries;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
        {
            var query = new GetCategoryByIdQuery(categoryId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Category does not exist");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel categoryCreateModel)
        {
            var command = new CreateCategoryCommand(categoryCreateModel);
            var result = await _mediator.Send(command);
            return result == true ? Ok("Created successfully") : BadRequest();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command);

            return result == true ? Ok("Deleted successfully.") : BadRequest();
        }
    }
}
