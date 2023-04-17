using KupujemProdajem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajem.Infrastructure.Context
{
    public class KupujemProdajemDbContext : IdentityDbContext<UserModel>
    {
        public KupujemProdajemDbContext(DbContextOptions<KupujemProdajemDbContext> options) : base(options)
        {
            
        }
    }
}
