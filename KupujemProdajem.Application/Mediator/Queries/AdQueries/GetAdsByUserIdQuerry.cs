using KupujemProdajem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Application.Mediator.Queries.AdQueries
{
    public class GetAdsByUserIdQuerry : IRequest<List<AdModel>>
    {
        public GetAdsByUserIdQuerry(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
