using LemmeProject.Application.Helpers;
using System.Text.Json.Serialization;

namespace LemmeProject.Application.DTOs.Images
{
    public class ProductImageAddRequest
    {

        public string FileName { get; set; }

        [JsonIgnore]
        public string FilePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "AppImages");
        public string FileBase64 { get; set; }

        //Relations
        [JsonIgnore]
        public int ProductId { get; set; }
    }
}
