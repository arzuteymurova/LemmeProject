using LemmeProject.Domain.Enums;

namespace LemmeProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }
        public string SkinType { get; set; }

        //Relations
        public List<Store> Stores { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductSearchHistory> ProductSearchHistory { get; set; }
    }
}
