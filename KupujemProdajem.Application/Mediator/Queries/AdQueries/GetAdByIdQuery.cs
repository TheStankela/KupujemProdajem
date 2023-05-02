using KupujemProdajem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Queries.AdQueries
{
    public class GetAdByIdQuery : IRequest<AdModel?>
    {
        public int Id { get; set; }
        public GetAdByIdQuery(int id) { Id = id; }
    }
}
