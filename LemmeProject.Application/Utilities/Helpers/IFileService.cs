using Microsoft.AspNetCore.Http;

namespace LemmeProject.Application.Utilities.Helpers
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
        byte[] GetImageAsync(string path);
    }
}
