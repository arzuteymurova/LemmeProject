using LemmeProject.Application.DTOs.Images;
using LemmeProject.Domain.Enums;

namespace LemmeProject.Application.DTOs.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }
        public SkinType SkinType { get; set; }


        //Relations
        public List<ProductImageAddRequest> Images { get; set; }
    }
}
