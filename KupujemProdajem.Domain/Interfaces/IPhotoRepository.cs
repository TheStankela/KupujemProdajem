using KupujemProdajem.Domain.Models;

namespace KupujemProdajem.Domain.Interfaces
{
    public interface IPhotoRepository
    {
        public Task<bool> AddPhoto(PhotoModel photoModel);
        public Task<bool> RemovePhoto(PhotoModel photoModel);
        public Task<List<PhotoModel>> GetAdPhotos(int adId);
    }
}
