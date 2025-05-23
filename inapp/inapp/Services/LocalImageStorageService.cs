using inapp.Interfaces.Services;
using System.Text;

namespace inapp.Services
{
    public class LocalImageStorageService : IImageStorageService
    {
        private readonly string _uploadsFolder;

        public LocalImageStorageService(IWebHostEnvironment env)
        {
            _uploadsFolder = Path.Combine(env.WebRootPath, "uploads");

            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var sb = new StringBuilder("/uploads/");
            sb.Append(fileName);
            return filePath;
        }
    }
}
