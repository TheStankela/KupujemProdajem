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
        public DbSet<AdModel> Ads { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<PhotoModel> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdModel>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
