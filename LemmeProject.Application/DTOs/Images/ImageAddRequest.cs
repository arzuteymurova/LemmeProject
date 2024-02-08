using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace LemmeProject.Application.DTOs.Images
{
    public class ImageAddRequest
    {
        public string FileName { get; set; }
        [JsonIgnore]
        public string FilePath { get; set; }
        public IFormFile File { get; set; }

        //Relations
        public int ProductId { get; set; }
    }
}
