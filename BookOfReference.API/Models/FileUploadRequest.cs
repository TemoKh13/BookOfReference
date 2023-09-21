namespace BookOfReference.API.Models
{
    public class FileUploadRequest
    {
        public IFormFile File { get; set; }

        public int Id { get; set; }
    }
}
