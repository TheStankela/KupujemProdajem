using KupujemProdajem.API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public CreateCategoryCommand(CreateCategoryModel categoryModel)
        {
            CategoryModel = categoryModel;
        }
        public CreateCategoryModel CategoryModel { get; set; }
    }
}
