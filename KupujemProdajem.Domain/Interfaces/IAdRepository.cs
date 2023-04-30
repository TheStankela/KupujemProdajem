using KupujemProdajem.Domain.Models;

namespace KupujemProdajem.Domain.Repositories
{
    public interface IAdRepository
    {
        Task<bool> CreateAdAsync(AdModel adModel);
        Task<bool> DeleteAd(AdModel adModel);
        Task<AdModel> GetAdByIdAsync(int id);
        Task<IEnumerable<AdModel>> GetAllAdsAsync();
        Task<bool> SaveAsync();
        Task<bool> UpdateAd(AdModel adModel);
        Task<List<AdModel>> GetAdsByCategory(int categoryId);
        Task<List<AdModel>> GetAdsByUserId(string userId);
    }
}