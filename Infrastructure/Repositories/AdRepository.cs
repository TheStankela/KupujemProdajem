using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using KupujemProdajem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajem.Infrastructure.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly KupujemProdajemDbContext _context;

        public AdRepository(KupujemProdajemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AdModel>> GetAllAdsAsync()
        {
            return await _context.Ads.ToListAsync();
        }
        public async Task<AdModel> GetAdByIdAsync(int id)
        {
            return await _context.Ads.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> CreateAdAsync(AdModel adModel)
        {
            await _context.Ads.AddAsync(adModel);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> DeleteAd(AdModel adModel)
        {
            _context.Remove(adModel);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAd(AdModel adModel)
        {
            _context.Update(adModel);
            return await SaveAsync();
        }
    }
}
