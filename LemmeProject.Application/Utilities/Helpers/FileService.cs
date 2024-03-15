using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LemmeProject.Application.Utilities.Helpers
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            try
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";

                var uploadsFolder = FileServerPath.Path;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                return $"{uniqueFileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return null;
            }
        }


        public byte[] GetImageAsync(string path)
        {
            string allPath=FileServerPath.Path+path;
            if (allPath != null)
            {
                return File.ReadAllBytes(allPath); 
            }

            return null; 
        }
    }
}
