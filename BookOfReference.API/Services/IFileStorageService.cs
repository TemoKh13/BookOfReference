namespace BookOfReference.API.Services
{
    public interface IFileStorageService
    {
        string StoreImage(string uniqueFileName, IFormFile file);
    }
}
