using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajem.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly KupujemProdajemDbContext _context;

        public PhotoRepository(KupujemProdajemDbContext context)
        {
            _context = context;
        }
        public async Task<List<PhotoModel>> GetAdPhotos(int adId)
        {
            return await _context.Photos.Where(p => p.AdId == adId).ToListAsync();
        }
        public async Task<bool> AddPhoto(PhotoModel photoModel)
        {
            await _context.AddAsync(photoModel);
            return await SaveAsync();
        }

        public async Task<bool> RemovePhoto(PhotoModel photoModel)
        {
            _context.Remove(photoModel);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
