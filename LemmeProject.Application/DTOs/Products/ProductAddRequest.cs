using LemmeProject.Application.DTOs.Images;
using LemmeProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace LemmeProject.Application.DTOs.Products
{
    public class ProductAddRequest
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }
        public string SkinType { get; set; }


        //Relations
        public List<IFormFile> Images { get; set; }
        public List<int> StoreIds { get; set; }
    }
}
