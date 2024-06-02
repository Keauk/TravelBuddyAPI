using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace TravelBuddyAPI.Controllers
{
    [Route("api/fileupload")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png"];
        private readonly List<string> _allowedMimeTypes = ["image/jpeg", "image/png"];
        private const long MaxFileSize = 10485760; // 10 MB

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            { 
                return BadRequest("File not selected");
            }

            if (file.Length > MaxFileSize)
            {
                return BadRequest("File size exceeds 10 MB limit");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(fileExtension) || !_allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
            {
                return BadRequest("Invalid file type.");
            }

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            // Generate a UUID for a unique filename
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var image = Image.Load(file.OpenReadStream()))
            {
                // Resize the image while maintaining aspect ratio
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 800),
                    Mode = ResizeMode.Max
                }));

                await image.SaveAsync(filePath);
            }

            var fileUrl = $"/uploads/{uniqueFileName}";
            return Ok(new { Url = fileUrl });
        }
    }
}