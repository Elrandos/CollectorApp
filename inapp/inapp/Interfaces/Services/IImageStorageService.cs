namespace inapp.Interfaces.Services
{
    public interface IImageStorageService
    {
        Task<string> SaveImageAsync(IFormFile file);
    }
}
