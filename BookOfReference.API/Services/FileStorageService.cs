namespace BookOfReference.API.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;
        private const string UploadsDirectory = "uploads";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
        }

        public string StoreImage(string uniqueFileName, IFormFile file)
        {
            var uploadsDirectory = Path.Combine(_webRootPath, UploadsDirectory);

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var imagePath = Path.Combine(uploadsDirectory, uniqueFileName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return Path.Combine(UploadsDirectory, uniqueFileName);
        }
    }
}

